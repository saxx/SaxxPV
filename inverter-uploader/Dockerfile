FROM python:latest

WORKDIR /app
RUN pip install goodwe
RUN pip install azure-storage-blob

COPY script.py /app/script.py

CMD ["python", "script.py"]

