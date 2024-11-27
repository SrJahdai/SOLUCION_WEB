$(document).ready(function () {
    // Botón de retroceso
    $("#back-button").click(function () {
      window.location.href = "anterior.html"; // Cambia por la ruta de la pantalla anterior
    });
  
    // Botón Ver Historial de Reportes
    $("#history-button").click(function () {
      window.location.href = "historial-reportes.html"; // Cambia por la ruta de historial de reportes
    });
  
    // Botón Ver Bauchers de Pago
    $("#vouchers-button").click(function () {
      window.location.href = "bauchers-pago.html"; // Cambia por la ruta de bauchers de pago
    });
  
    // Cargar datos dinámicos desde la base de datos (simulado)
    const reportData = {
      reportRange: "01 / 11 / 2024 - 30 / 11 / 2024",
      rentedProfit: "5000$",
      rentedProvider: "2000$",
      grossRented: "3000$",
      soldProfit: "8000$",
      soldProvider: "4000$",
      grossSold: "4000$",
      generalProfit: "13000$",
      grossProfit: "7000$",
    };
  
    // Insertar datos dinámicos en el DOM
    $("#report-range").text(reportData.reportRange);
    $("#rented-profit").text(reportData.rentedProfit);
    $("#rented-provider").text(reportData.rentedProvider);
    $("#gross-rented").text(reportData.grossRented);
    $("#sold-profit").text(reportData.soldProfit);
    $("#sold-provider").text(reportData.soldProvider);
    $("#gross-sold").text(reportData.grossSold);
    $("#general-profit").text(reportData.generalProfit);
    $("#gross-profit").text(reportData.grossProfit);
  });
  