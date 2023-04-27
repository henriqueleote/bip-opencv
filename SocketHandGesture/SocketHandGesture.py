import cv2
import mediapipe as mp
import socket

mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands

cap = cv2.VideoCapture(0)

socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5052)

with mp_hands.Hands(min_detection_confidence=0.5, min_tracking_confidence=0.5, max_num_hands=1) as hands_detection:
    while cap.isOpened():
        ret, frame = cap.read()

        # Flip the frame horizontally for a mirrored view
        frame = cv2.flip(frame, 1)

        # Get the height and width of the frame
        height, width, _ = frame.shape

        # Calculate the section boundaries
        left_boundary = 0
        right_boundary = int(width * 0.33)
        middle_boundary = int(width * 0.67)

        # Convert the frame to RGB for processing with MediaPipe
        rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

        # Detect the hands in the frame
        results = hands_detection.process(rgb_frame)

        # Draw the section boundaries on the frame
        cv2.rectangle(frame, (left_boundary, 0), (right_boundary, height), (0, 0, 0), 2)
        cv2.rectangle(frame, (right_boundary, 0), (middle_boundary, height), (0, 0, 0), 2)
        cv2.rectangle(frame, (middle_boundary, 0), (width, height), (0, 0, 0), 2)

        # Check if the right hand was detected
        if results.multi_hand_landmarks and results.multi_handedness[0].classification[0].label == 'Right':
            # Get the coordinates of the first hand landmark
            hand_landmarks = results.multi_hand_landmarks[0]
            x, y = int(hand_landmarks.landmark[0].x * frame.shape[1]), int(hand_landmarks.landmark[0].y * frame.shape[0])
            position = (x, y)
            print(position)
            socket.sendto(str.encode(str(position)), serverAddressPort)
            # Check if the hand is closed
            if hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_TIP].y > hand_landmarks.landmark[mp_hands.HandLandmark.INDEX_FINGER_MCP].y and \
                    hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_TIP].y > hand_landmarks.landmark[mp_hands.HandLandmark.MIDDLE_FINGER_MCP].y and \
                    hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_TIP].y > hand_landmarks.landmark[mp_hands.HandLandmark.RING_FINGER_MCP].y and \
                    hand_landmarks.landmark[mp_hands.HandLandmark.PINKY_TIP].y > hand_landmarks.landmark[mp_hands.HandLandmark.PINKY_MCP].y:
                print("jump")
                socket.sendto(str.encode(str("jump")), serverAddressPort)

            # Draw the hand landmarks on the frame
            mp_drawing.draw_landmarks(frame, hand_landmarks, mp_hands.HAND_CONNECTIONS)


        # Display the frame
        cv2.imshow('Hand Detection', frame)

        # Exit on key press
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

cap.release()
cv2.destroyAllWindows()
