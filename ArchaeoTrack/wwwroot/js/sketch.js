window.initializeCanvas = function () {
    let canvas = document.getElementById('sketchCanvas');
    let ctx = canvas.getContext('2d');

    let drawing = false;

    // Start drawing
    canvas.addEventListener('mousedown', function (e) {
        drawing = true;
        ctx.beginPath();
        ctx.moveTo(e.offsetX, e.offsetY);
    });

    // Draw on the canvas
    canvas.addEventListener('mousemove', function (e) {
        if (drawing) {
            ctx.lineTo(e.offsetX, e.offsetY);
            ctx.stroke();
        }
    });

    // Stop drawing
    canvas.addEventListener('mouseup', function () {
        drawing = false;
        ctx.closePath();
    });

    // Clear the canvas on double-click
    canvas.addEventListener('dblclick', function () {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    });
};

// Function to get the canvas image as Base64
window.getCanvasImage = function () {
    let canvas = document.getElementById('sketchCanvas');
    return canvas.toDataURL(); // Return the Base64 image
};
