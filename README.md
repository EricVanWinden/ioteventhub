# iothub
Repo consists of 2 parts.

The first part is the Python file that reads environment data from the Raspberry Pi Sensehat to the IoT hub.

The second part is the .Net Core application that processes the events send by the IoT hub. The data is averaged per hour and stored in a json file. This file is opened when the aplication starts and updated every time the event processing is stopped. Data is visualized with a very basic OxyPlot chart.