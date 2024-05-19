from ultralytics import YOLO
from PIL import Image
from shiftlab_ocr.doc2text.reader import Reader
import json
from flask import Flask, request

paths = []

model = YOLO('last.pt')
r = Reader()
app = Flask("My Recognizer")

@app.route("/process", methods=['POST'])
def HandleRequest():
    paths = request.data.decode().split(';')
    print(f"Request contents: {paths}")
    data = {}
    for a in paths:
        data[a] = {'hitbox': None, 'class': None, 'text': None}
        results = model.predict(a)
        im = Image.open(a)
        for result in results:
            boxes = result.boxes
            records = []
            for box in boxes:
                className = result.names[int(box.cls[0])]
                # Угадай буквы
                #ХВатит торчатьы
                x,y,w,h = map(int, (box.xywh)[0])
                im.crop((x,y, x+w,y+h)).convert('RGB').save('cur.jpeg')
                text = r.doc2text('cur.jpeg')
                records.append({
                    'hitbox': (x,y,w,h),
                    'class': className,
                    'text': text[0],
                })
            data[a] = records
    json_data = json.dumps(data)
    return json_data

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=42513)