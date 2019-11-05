function genScreenshot() {
    html2canvas(document.body).then(canvas => {
        try {
            window.ScreenShot = canvas.toDataURL('image/png');
        }
        catch{
            window.ScreenShot = "error";
        }
    });
}

genScreenshot();