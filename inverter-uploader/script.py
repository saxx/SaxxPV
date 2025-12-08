import asyncio
import goodwe
import time
import json
from azure.storage.blob import BlobServiceClient

async def get_runtime_data():
    ip_address = ''

    inverter = await goodwe.connect(ip_address)
    runtime_data = await inverter.read_runtime_data()

    data = {}

    for sensor in inverter.sensors():
        if sensor.id_ in runtime_data:
            data[sensor.name] = f"{runtime_data[sensor.id_]} {sensor.unit}"

    timestamp = int(time.time())
    text = json.dumps(data)

    connection_string = ""
    service_client = BlobServiceClient.from_connection_string(connection_string)
    container_name = "inverter-uploads"
    container_client = service_client.get_container_client(container_name)
    container_client.upload_blob(name=f"{timestamp}.json", data=text, overwrite=True)

while True:
    try:
        print(f"Fetching & uploading ...", flush = True)
        asyncio.run(get_runtime_data())
        print(f"Done. Sleeping ...", flush = True)
        time.sleep(60)

    except Exception as ex:
        print(f"Ein Unglück ist widerfahren: {ex}", flush = True)
