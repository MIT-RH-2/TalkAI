#dependencies
import nltk
import speech_recognition as sr
from nltk.sentiment.vader import SentimentIntensityAnalyzer

#one-time download
#nltk.download('vader_lexicon')

#takes in wav and returns audio transcribed script
def transcription(recognizer, audio_bytes):
    audio_source = sr.AudioData(audio_bytes, sample_rate = 16000, sample_width = 2)
    text = recognizer.recognize_google(audio_data=audio_source, show_all= False);
    return text;

#takes in transcribed script and returns detected emotion
def emotions(script):
    sid = SentimentIntensityAnalyzer()
    emotion_mat = sid.polarity_scores(script)
    if emotion_mat['compound'] > 0.6:
        return 'surprised'
    elif emotion_mat['compound'] > 0.4:
        return 'happy'
    elif emotion_mat['compound'] > 0.2:
        return 'engaged'
    elif emotion_mat['compound'] < -0.6:
        return 'angry'
    elif emotion_mat['compound'] < -0.2:
        return 'sad'
    else: return 'neutral'
    
if __name__ == "__main__":
    
    recognizer = sr.Recognizer()

    with open("input_examples/user_input_1.wav", 'rb') as f:
        audio_bytes = f.read();
        
    text = transcription(recognizer, audio_bytes)
    print(text)
    emotion = emotions(text)
    print(emotion)


