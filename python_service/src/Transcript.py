
import speech_recognition as sr


def get_transcript(recognizer, audio_bytes):
    
    audio_source = sr.AudioData(audio_bytes, sample_rate = 16000, sample_width = 2)
    text = recognizer.recognize_google(audio_data=audio_source, show_all= False);
    return text;

def extract_emotion(transcript): # For testing only, not being used
    words = transcript.split();
    words = [word.strip().lower() for word in words]
   
    for keyword in ["happy", "funny", "welcome", "wonderful", "cool", "smile", "smiling", "thank"]:
        if keyword in words:
            return "happy";
    for keyword in ["surprise", "surprised"]:
        if keyword in words:
            return "surprise";
    for keyword in ["sad", "cry", "crying"]:
        if keyword in words:
            return "sad";
    for keyword in ["upset", "angry"]:
        if keyword in words:
            return "angry";
    return "neutral"

if __name__ == "__main__":
    
    recognizer = sr.Recognizer()

    with open("input_examples/user_input_1.wav", 'rb') as f:
        audio_bytes = f.read();
        
    text = get_transcript(recognizer, audio_bytes)
    print(text)
    emotion = extract_emotion(text)
    print(emotion)
        
    # surprise, happy, engaged, neutral, sad, angry