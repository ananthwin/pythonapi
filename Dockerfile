FROM python:3

WORKDIR /usr/ANANTH/Desktop/Python/PythonAPI

COPY requirements.txt ./
RUN pip install --no-cache-dir -r requirements.txt

COPY . .

CMD [ "python", "./hello.py" ]

