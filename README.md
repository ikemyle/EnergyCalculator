1.  Building Requirements
a.   To load and build the application we need VS 2022 with ASp .Net core
2.  Basic Architecture
a.   The application is built on top of ASP net core 8 (migrated) using for the UI jquery and Bootstrap.
b.   The backend code is well structured in a different project library (EM.Core), so the code is reusable.
c.    For the web socket a middleware has been implemented that takes care of the socket connection as well as the message communication
d.   Communication between client and server, has been completed through Ajax and rest api services.
3.  User guide/interface
a.   Home page holds two functions: “Power Load” and “CO2 Emission.”
![image](https://github.com/ikemyle/EnergyCalculator/assets/975391/ddc26723-e5f9-4fdc-b693-fd538e2ea7ad)

b.   PowerLoad page is holding main functionalities, including Json Data load, ajax calls as well as web socket communication.
![image](https://github.com/ikemyle/EnergyCalculator/assets/975391/72b83ea9-1847-4c4d-9963-fe3f17e6e8dd)

Note: There are three default json files that have been preloaded. However, data can be pasted and loaded from the Json Load textbox itself.
Upon processing and computation, the program will display friendly alerts as successful or warnings in case the produced energy does not cover the energy need.
![image](https://github.com/ikemyle/EnergyCalculator/assets/975391/f7418130-036c-4bb1-8f4b-50b125cd47fc)

c.    CO2 Emission page it displays the data only when a json load is pushed to the server. Since communication supports ws protocol, the client will be updated right away. 
In order to test this functionality, two browsers can be open, one loading power load and the other Co2 Emission. Once we push the load, we will see the data update on CO2 emission page.
![image](https://github.com/ikemyle/EnergyCalculator/assets/975391/3b016d4a-3674-44f0-a0e2-ff1da7acef43)

4.  Developer guide
a.   The computing functionality has been decoupled into a different project called EM.Core (initials from energy management). We master the following features:
i.   Computation of needed energy based on power plants merit type.
ii.   Computation of CO2 Emission individually as well as the total
iii.   Computation of cost individually as well as the total
iv.   Final returned list ordered by merit/quantity
b.   Data loading, serialization and deserialization has been completed through a rest api service presented under Api folder, as PowerController. Since ASP Net core is doing a great job having built in functionality of dependency injection, an interface IpowerComputation has been implemented and registered in startup.
c.    Socket communication has been implemented as a middleware but does not have any heavy rules. Communication is just simple, transfering messages back and forth between clients.
d.   A basic logger has been implemented. However, it definetilly will be needed to persist it into a database or/and foile system.
e.   XML comments have been added to most of the classes.
f.    UI script is being contained in two javascript files in wwwroot under js folder, co2socket.js and power.js. Since that was not the major part of the project was kept simple. Ultimatelly a better aproach will be to use a javascript framework as Angular or Knockout. However, a nice javascript component has been used to display tabular data: DataTables.net.
5.  Notes
Other frameworks being used are Jquery and Bootstrap, which personally I found always necessary for a good responsive UI. Also, persistent logging and error handling needs to be implemented if this will be a final product.
