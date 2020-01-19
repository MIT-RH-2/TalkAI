#handoff code
import librosa
import soundfile as sf
import os, glob, pickle
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.neural_network import MLPClassifier
from sklearn.metrics import accuracy_score
import io

count = 0;
#render audio and run model
#NOTE to load audio file in WAV
def extract_emotion(pickle_model, audio_bytes):
    feature = extract_feature(audio_bytes, mfcc=True, chroma=True, mel=True)
    emotion_pred = pickle_model.predict([feature])
    return emotion_pred

#Extract features function
def extract_feature(data_bytes, mfcc, chroma, mel):
    global count
    count = count + 1;
#     with open('user_inputs/user_input_' + str(count) + '.wav', 'wb') as f:
#         f.write(data_bytes)
    
    data, samplerate = sf.read(io.BytesIO(data_bytes))
#     with soundfile.SoundFile(file_name) as sound_file: 
#     X = data.read(dtype="float32")
    X = data
    sample_rate = samplerate
    print("sample_rate:", sample_rate)
    if chroma:
        stft=np.abs(librosa.stft(X))
        result=np.array([])
    if mfcc:
        mfccs=np.mean(librosa.feature.mfcc(y=X, sr=sample_rate, n_mfcc=40).T, axis=0)
        result=np.hstack((result, mfccs))
    if chroma:
        chroma=np.mean(librosa.feature.chroma_stft(S=stft, sr=sample_rate).T,axis=0)
        result=np.hstack((result, chroma))
    if mel:
        mel=np.mean(librosa.feature.melspectrogram(X, sr=sample_rate).T,axis=0)
        result=np.hstack((result, mel))
    return result    
    
#load pickle model file
def load_model(pkl_filename):
    with open(pkl_filename, 'rb') as file:
        pickle_model = pickle.load(file)
    return pickle_model


if __name__ == "__main__":
    pickle_model = load_model("realityhack_modelnew.pkl")
#     emotion = extract_emotion(pickle_model, '/Users/edo/Documents/workspace_oxygen_hackathon/hackathon/input_examples/03-01-01-01-01-01-01.wav')
#     print(emotion)
    
    with open('input_examples/user_input_1.wav', "rb") as f:
        bytes_read = f.read()
    print("len:", len(bytes_read))
        
    emotion = extract_emotion(pickle_model, bytes_read)
    print(emotion)
    
    
    
    
    
    
