# Downloaded from https://docs.microsoft.com/nl-nl/azure/iot-hub/quickstart-send-telemetry-python
# Copyright (c) Microsoft. All rights reserved.
# Licensed under the MIT license. See LICENSE file in the project root for full license information.

import time
from sense_hat import SenseHat
from azure.iot.device import IoTHubDeviceClient, Message

# The device connection string to authenticate the device with your IoT hub.
# Using the Azure CLI:
# az iot hub device-identity show-connection-string --hub-name {YourIoTHubName} --device-id MyNodeDevice --output table
CONNECTION_STRING = "HostName=ericsweatherhub.azure-devices.net;DeviceId=sensehat;SharedAccessKey=8d1UQifaoO558pY1uUMj7CrCjfbaX2kDwMt5QA39Uv4="

# Define the JSON message to send to IoT Hub.
MSG_TXT = '{{"temperature": {temperature},"humidity": {humidity},"pressure": {pressure}}}'


def iothub_client_init():
    # Create an IoT Hub client
    client = IoTHubDeviceClient.create_from_connection_string(CONNECTION_STRING)
    return client


def iothub_client_telemetry_sample_run():
    on = [255, 0, 0]
    off = [0, 0, 0]
    pixels_off = [
        off, off, off, off, off, off, off, off,
        off, off, off, off, off, off, off, off,
        off, off, off, off, off, off, off, off,
        off, off, off, off, off, off, off, off,
        off, off, off, off, off, off, off, off,
        off, off, off, off, off, off, off, off,
        off, off, off, off, off, off, off, off,
        off, off, off, off, off, off, off, off]

    sense = SenseHat()
    sense.set_pixels(pixels_off)
    try:
        client = iothub_client_init()
        print("IoT Hub device sending periodic messages, press Ctrl-C to exit")

        while True:
            # Build the message with sense hat telemetry values.
            sense.set_pixel(0, 0, on)
            temperature = sense.get_temperature()
            humidity = sense.get_humidity()
            pressure = sense.get_pressure()
            msg_txt_formatted = MSG_TXT.format(temperature=temperature, humidity=humidity, pressure=pressure)
            message = Message(msg_txt_formatted)

            # Send the message.
            print("Sending message: {}".format(message))
            client.send_message(message)
            print("Message successfully sent")
            sense.set_pixel(0, 0, off)
            time.sleep(30)

    except KeyboardInterrupt:
        print("IoTHubClient sample stopped")


if __name__ == '__main__':
    print("IoT Hub Quickstart #1 - Simulated device")
    print("Press Ctrl-C to exit")
    iothub_client_telemetry_sample_run()
