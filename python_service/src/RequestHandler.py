
from http.server import BaseHTTPRequestHandler,HTTPServer
# import SpeechExtraction
import nltk
import speech_recognition as sr

HOST_NAME = ''
PORT_NUMBER = 80
import BoW


class MyHandler(BaseHTTPRequestHandler):
    
#     pickle_model = SpeechExtraction.load_model("realityhack_modelnew.pkl")
    recognizer = sr.Recognizer()
    #one-time download
    nltk.download('vader_lexicon')
    
    def do_GET(self):
        print  ('Get request received')
        self.send_response(200)
        self.send_header("Content-type", "text/plain")
        self.end_headers()
        self.wfile.write(bytearray(b'testing'))
        
    def do_POST(self):
        print ('Post request received. New code')

        length = int(self.headers.get('Content-length', 0))
        print ('length: ',length)
        body = self.rfile.read(length)

        bytes_read = bytearray(body)
        text = BoW.transcription(MyHandler.recognizer, bytes_read)
        print(text)
        emotion = BoW.emotions(text)
        print(emotion)
        
        self.send_response(200)
        self.send_header("Content-type", "text/plain")
        self.end_headers()
        self.wfile.write(bytearray(emotion.encode('utf-8')))   
        
#     def do_POST_did_not_work(self):
#         print ('Post request received')
# 
#         length = int(self.headers.get('Content-length', 0))
#         print ('length: ',length)
#         body = self.rfile.read(length)
# 
#         bytes_read = bytearray(body)
#         emotions = SpeechExtraction.extract_emotion(MyHandler.pickle_model, bytes_read)
#         print(emotions)
#         
#         self.send_response(200)
#         self.send_header("Content-type", "text/plain")
#         self.end_headers()
#         self.wfile.write(bytearray(emotions[0].encode('utf-8')))
        

if __name__ == '__main__':
    server_class = HTTPServer
    httpd = server_class((HOST_NAME, PORT_NUMBER), MyHandler)
    print("Server is running on port ", PORT_NUMBER)
    try:
        httpd.serve_forever()
    except KeyboardInterrupt:
        pass
    httpd.server_close()
