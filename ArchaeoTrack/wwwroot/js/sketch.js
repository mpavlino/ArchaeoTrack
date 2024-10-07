window.initializeCanvas = function () {
    let canvas = document.getElementById('sketchCanvas');
    console.log(canvas);  // Check if canvas is found
    let ctx;
    if (canvas) {
        ctx = canvas.getContext('2d');
    } else {
        console.error("Canvas not found");
        return;
    }

    let drawing = false;
    let startX, startY;
    let shapeMode = 'freeform'; // Modes: 'freeform', 'line', 'circle', 'rectangle', 'eraser'
    let penColor = '#000000';   // Default color: black
    let penSize = 2;            // Default pen size
    ctx.lineWidth = penSize;

    // Function to set drawing mode (pen/eraser)
    window.setShapeMode = function (mode) {
        shapeMode = mode;
        if (mode === 'eraser') {
            ctx.strokeStyle = '#FFFFFF'; // Eraser: white color to simulate clearing
            ctx.lineWidth = penSize;
        } else {
            ctx.strokeStyle = penColor; // Reset to pen color
        }
    };

    // Function to set pen color
    window.setPenColor = function (color) {
        penColor = color;
        if (shapeMode !== 'eraser') {
            ctx.strokeStyle = penColor;
        }
    };

    // Function to set pen size
    window.setPenSize = function (size) {
        penSize = size;
        ctx.lineWidth = penSize;
    };

    // Start drawing
    function startDrawing(e) {
        drawing = true;
        const pos = getMousePos(e);
        startX = pos.x;
        startY = pos.y;
        ctx.beginPath();
        if (shapeMode === 'freeform') {
            ctx.moveTo(startX, startY);
        }
    }

    // Draw on the canvas
    function draw(e) {
        if (drawing) {
            const pos = getMousePos(e);
            if (shapeMode === 'freeform' || shapeMode === 'eraser') {
                ctx.lineTo(pos.x, pos.y);
                ctx.stroke();
            }
        }
    }

    // Stop drawing and finalize shape
    function stopDrawing(e) {
        if (!drawing) return;
        drawing = false;
        ctx.closePath();

        const pos = getMousePos(e);
        if (shapeMode === 'line') {
            // Draw a straight line
            ctx.beginPath();
            ctx.moveTo(startX, startY);
            ctx.lineTo(pos.x, pos.y);
            ctx.stroke();
            ctx.closePath();
        } else if (shapeMode === 'circle') {
            // Draw a circle
            let radius = Math.sqrt(Math.pow(pos.x - startX, 2) + Math.pow(pos.y - startY, 2));
            ctx.beginPath();
            ctx.arc(startX, startY, radius, 0, 2 * Math.PI);
            ctx.stroke();
            ctx.closePath();
        } else if (shapeMode === 'rectangle') {
            // Draw a rectangle
            let rectWidth = pos.x - startX;
            let rectHeight = pos.y - startY;
            ctx.beginPath();
            ctx.rect(startX, startY, rectWidth, rectHeight);
            ctx.stroke();
            ctx.closePath();
        }
    }

    // Get mouse position relative to the canvas
    function getMousePos(e) {
        const rect = canvas.getBoundingClientRect();
        let x, y;

        if (e.touches) {
            // Handle touch events
            x = e.touches[0].clientX - rect.left;
            y = e.touches[0].clientY - rect.top;
        } else {
            // Handle mouse events
            x = e.clientX - rect.left;
            y = e.clientY - rect.top;
        }

        // Scale the touch/mouse coordinates to account for any scaling of the canvas
        const scaleX = canvas.width / rect.width;
        const scaleY = canvas.height / rect.height;

        return {
            x: (x * scaleX),
            y: (y * scaleY)
        };
    }


    // Add mouse event listeners
    canvas.addEventListener('mousedown', startDrawing);
    canvas.addEventListener('mousemove', draw);
    canvas.addEventListener('mouseup', stopDrawing);
    canvas.addEventListener('mouseleave', stopDrawing); // Stop drawing if mouse leaves canvas

    // Add touch event listeners
    canvas.addEventListener('touchstart', function (e) {
        e.preventDefault(); // Prevent scrolling
        startDrawing(e);
    });
    canvas.addEventListener('touchmove', function (e) {
        e.preventDefault(); // Prevent scrolling
        draw(e);
    });
    canvas.addEventListener('touchend', stopDrawing);

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

// Function to clear the canvas
window.clearCanvas = function () {
    let canvas = document.getElementById('sketchCanvas');
    let ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
};
