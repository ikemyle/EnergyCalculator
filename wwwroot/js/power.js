function Power() { }

Power.PostLoad = function (index) {
    var load;
    switch (index) {
        case (1):
            load = Power.load1();
            break;

        case (2):
            load = Power.load2();
            break;

        case (3):
            load = Power.load3();
            break;

        default:
            if ($("#jsonload").val() !== '') {
                load = JSON.parse($("#jsonload").val());
            }
    }

    if (index !== 0) {
        $("#jsonload").val(JSON.stringify(load));
    }

    if (load == null) {
        alert("Invalid input");
        return;
    }

    $("#waitingOverlay").show();
    $("#ajaxloader").show();
    $.ajax({
        url: "/api/power/",
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(load),
        headers: { 'Accept': 'application/json' },
        error: function (returnval) {
            alert(returnval.responseText);
        },
        success: function (response) {            
            $("#jsonresponse").text(JSON.stringify(response));
            if ($.fn.DataTable.isDataTable('#pwDistribution')) {
                $('#pwDistribution').DataTable().destroy();
            }
            //get total
            var total = 0;
            for (var i = 0; i < response.length; i++) {
                total += response[i].p;
            }
            $("#powerTotalSuccess").text("Needed: " + load.load + " Generated: " + total);
            $("#powerTotalError").text("Needed: " + load.load + " Generated: " + total);
            if (total >= load.load) {
                $("#alertSuccess").show();
                $("#alertError").hide();
            }
            else {
                $("#alertSuccess").hide();
                $("#alertError").show();
            }
            var distTable = $("#pwDistribution").DataTable({
                data: response,
                bSort: false,
                columns: [
                    { "data": "name" },
                    { "data": "p" }
                ]
            });
            $("#waitingOverlay").hide();
            $("#ajaxloader").hide();
            Power.GetCo2();
        }
    });
};

Power.GetCo2 = function () {
    $.ajax({
        url: "/api/power/",
        type: "get",
        contentType: "application/json",
        headers: { 'Accept': 'application/json' },
        error: function (returnval) {
            alert(returnval.responseText);
        },
        success: function (response) {
            //send the co2 request
            sender(response);
        }
    });
}

Power.load1 = function () {
    var load1 = {
        "load": 480,
        "fuels":
        {
            "gas(euro/MWh)": 13.4,
            "kerosine(euro/MWh)": 50.8,
            "co2(euro/ton)": 20,
            "wind(%)": 60
        },
        "powerplants": [
            {
                "name": "gasfiredbig1",
                "type": "gasfired",
                "efficiency": 0.53,
                "pmin": 100,
                "pmax": 460
            },
            {
                "name": "gasfiredbig2",
                "type": "gasfired",
                "efficiency": 0.53,
                "pmin": 100,
                "pmax": 460
            },
            {
                "name": "gasfiredsomewhatsmaller",
                "type": "gasfired",
                "efficiency": 0.37,
                "pmin": 40,
                "pmax": 210
            },
            {
                "name": "tj1",
                "type": "turbojet",
                "efficiency": 0.3,
                "pmin": 0,
                "pmax": 16
            },
            {
                "name": "windpark1",
                "type": "windturbine",
                "efficiency": 1,
                "pmin": 0,
                "pmax": 150
            },
            {
                "name": "windpark2",
                "type": "windturbine",
                "efficiency": 1,
                "pmin": 0,
                "pmax": 36
            }
        ]
    };
    return load1;
};

Power.load2 = function () {
    var load2 = {
        "load": 480,
        "fuels":
        {
            "gas(euro/MWh)": 13.4,
            "kerosine(euro/MWh)": 50.8,
            "co2(euro/ton)": 20,
            "wind(%)": 0
        },
        "powerplants": [
            {
                "name": "gasfiredbig1",
                "type": "gasfired",
                "efficiency": 0.53,
                "pmin": 100,
                "pmax": 460
            },
            {
                "name": "gasfiredbig2",
                "type": "gasfired",
                "efficiency": 0.53,
                "pmin": 100,
                "pmax": 460
            },
            {
                "name": "gasfiredsomewhatsmaller",
                "type": "gasfired",
                "efficiency": 0.37,
                "pmin": 40,
                "pmax": 210
            },
            {
                "name": "tj1",
                "type": "turbojet",
                "efficiency": 0.3,
                "pmin": 0,
                "pmax": 16
            },
            {
                "name": "windpark1",
                "type": "windturbine",
                "efficiency": 1,
                "pmin": 0,
                "pmax": 150
            },
            {
                "name": "windpark2",
                "type": "windturbine",
                "efficiency": 1,
                "pmin": 0,
                "pmax": 36
            }
        ]
    };
    return load2;
};

Power.load3 = function () {
    var load3 = {
        "load": 910,
        "fuels":
        {
            "gas(euro/MWh)": 13.4,
            "kerosine(euro/MWh)": 50.8,
            "co2(euro/ton)": 20,
            "wind(%)": 60
        },
        "powerplants": [
            {
                "name": "gasfiredbig1",
                "type": "gasfired",
                "efficiency": 0.53,
                "pmin": 100,
                "pmax": 460
            },
            {
                "name": "gasfiredbig2",
                "type": "gasfired",
                "efficiency": 0.53,
                "pmin": 100,
                "pmax": 460
            },
            {
                "name": "gasfiredsomewhatsmaller",
                "type": "gasfired",
                "efficiency": 0.37,
                "pmin": 40,
                "pmax": 210
            },
            {
                "name": "tj1",
                "type": "turbojet",
                "efficiency": 0.3,
                "pmin": 0,
                "pmax": 16
            },
            {
                "name": "windpark1",
                "type": "windturbine",
                "efficiency": 1,
                "pmin": 0,
                "pmax": 150
            },
            {
                "name": "windpark2",
                "type": "windturbine",
                "efficiency": 1,
                "pmin": 0,
                "pmax": 36
            }
        ]
    }
        ;
    return load3;
};

$(document).ready(function () {
    $("#btnLoadFromText").click(function () {
        Power.PostLoad(0);
        return false;
    });
});