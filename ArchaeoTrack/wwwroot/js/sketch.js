let drawing = false;
let startX, startY;
let shapeMode = 'freeform'; // Modes: 'freeform', 'line', 'circle', 'rectangle', 'eraser'
let penColor = '#000000';   // Default color: black
let penSize = 2;            // Default pen size
let shapes = [];            // Array to store drawn shapes
let freeformPaths = [];     // Array to store freeform paths with their pen size and color
let undoStack = [];         // Stack for undo actions
let redoStack = [];         // Stack for redo actions
let canvas, ctx;

// Initialize the canvas
window.initializeCanvas = function () {
    canvas = document.getElementById('sketchCanvas');
    if (!canvas) {
        console.error("Canvas not found");
        return;
    }
    canvas.width = canvas.offsetWidth;
    canvas.height = canvas.offsetHeight;
    ctx = canvas.getContext('2d');

    // Clear the canvas and reset arrays on initialization
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    shapes = [];
    freeformPaths = [];
    undoStack = [];
    redoStack = [];

    // Set default pen size and color
    ctx.lineWidth = penSize;
    ctx.strokeStyle = penColor;

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
    canvas.addEventListener('touchend', function (e) {
        e.preventDefault(); // Finalize drawing
        stopDrawing(e);
    });
};

// Set drawing mode (pen/eraser)
window.setShapeMode = function (mode) {
    shapeMode = mode;
    if (mode === 'eraser') {
        ctx.strokeStyle = '#FFFFFF'; // Eraser: white color to simulate clearing
    } else {
        ctx.strokeStyle = penColor; // Reset to pen color
    }
    ctx.lineWidth = penSize; // Set line width to current pen size
};

// Set pen color
window.setPenColor = function (color) {
    penColor = color;
    if (shapeMode !== 'eraser') {
        ctx.strokeStyle = penColor; // Update the stroke style
    }
};

// Set pen size
window.setPenSize = function (size) {
    penSize = size; // Update the pen size
    ctx.lineWidth = penSize; // Update the line width for current drawing mode
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
        freeformPaths.push({ points: [{ x: startX, y: startY }], color: penColor, size: penSize }); // Store color and size with path
    }
}

// Draw on the canvas
function draw(e) {
    const pos = getMousePos(e);
    if (drawing) {
        if (shapeMode === 'freeform' || shapeMode === 'eraser') {
            ctx.lineTo(pos.x, pos.y);
            ctx.stroke();
            if (shapeMode === 'freeform') {
                const currentPath = freeformPaths[freeformPaths.length - 1];
                currentPath.points.push({ x: pos.x, y: pos.y }); // Store the point
            }
        } else {
            // Update pen size and color in real-time for shape preview
            ctx.save(); // Save the current canvas state
            ctx.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas for a clean preview
            redrawAllShapes(); // Redraw all existing shapes
            redrawAllFreeform(); // Redraw all freeform paths
            ctx.lineWidth = penSize;
            ctx.strokeStyle = penColor;

            ctx.beginPath();
            if (shapeMode === 'line') {
                ctx.moveTo(startX, startY);
                ctx.lineTo(pos.x, pos.y);
            } else if (shapeMode === 'circle') {
                let radius = Math.sqrt(Math.pow(pos.x - startX, 2) + Math.pow(pos.y - startY, 2));
                ctx.arc(startX, startY, radius, 0, 2 * Math.PI);
            } else if (shapeMode === 'rectangle') {
                let rectWidth = pos.x - startX;
                let rectHeight = pos.y - startY;
                ctx.rect(startX, startY, rectWidth, rectHeight);
            }
            ctx.stroke();
            ctx.restore(); // Restore the canvas state
        }
    }
}

// Finalize the shape when drawing stops
function stopDrawing(e) {
    if (!drawing) return;
    drawing = false;
    const pos = getMousePos(e);
    ctx.closePath();

    let currentAction = {
        shapes: [...shapes],
        freeformPaths: [...freeformPaths],
    };

    if (shapeMode === 'line') {
        shapes.push({ shape: 'line', startX, startY, endX: pos.x, endY: pos.y, color: penColor, size: penSize });
    } else if (shapeMode === 'circle') {
        let radius = Math.sqrt(Math.pow(pos.x - startX, 2) + Math.pow(pos.y - startY, 2));
        shapes.push({ shape: 'circle', x: startX, y: startY, radius, color: penColor, size: penSize });
    } else if (shapeMode === 'rectangle') {
        let rectWidth = pos.x - startX;
        let rectHeight = pos.y - startY;
        shapes.push({ shape: 'rectangle', x: startX, y: startY, width: rectWidth, height: rectHeight, color: penColor, size: penSize });
    }

    undoStack.push(currentAction); // Push current action to undo stack
    redoStack = []; // Clear the redo stack
}

// Redraw the canvas by clearing and redrawing shapes and paths
function redrawCanvas() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    redrawAllShapes();
    redrawAllFreeform();
}

// Function to redraw all freeform paths
function redrawAllFreeform() {
    for (const path of freeformPaths) {
        ctx.beginPath();
        ctx.lineWidth = path.size; // Use the stored pen size
        ctx.strokeStyle = path.color; // Use the stored pen color
        ctx.moveTo(path.points[0].x, path.points[0].y);
        for (let point of path.points) {
            ctx.lineTo(point.x, point.y);
        }
        ctx.stroke();
        ctx.closePath();
    }
}

// Function to redraw all shapes
function redrawAllShapes() {
    for (const shape of shapes) {
        ctx.beginPath();
        ctx.lineWidth = shape.size;
        ctx.strokeStyle = shape.color;
        if (shape.shape === 'line') {
            ctx.moveTo(shape.startX, shape.startY);
            ctx.lineTo(shape.endX, shape.endY);
        } else if (shape.shape === 'circle') {
            ctx.arc(shape.x, shape.y, shape.radius, 0, 2 * Math.PI);
        } else if (shape.shape === 'rectangle') {
            ctx.rect(shape.x, shape.y, shape.width, shape.height);
        }
        ctx.stroke();
        ctx.closePath();
    }
}

// Get mouse position relative to the canvas
function getMousePos(e) {
    const rect = canvas.getBoundingClientRect();
    let x, y;

    // Use changedTouches for touchend to get the last touch point
    if (e.changedTouches && e.changedTouches.length > 0) {
        x = e.changedTouches[0].clientX - rect.left;
        y = e.changedTouches[0].clientY - rect.top;
    } else if (e.touches && e.touches.length > 0) {
        x = e.touches[0].clientX - rect.left;
        y = e.touches[0].clientY - rect.top;
    } else {
        x = e.clientX - rect.left;
        y = e.clientY - rect.top;
    }

    // Scale the coordinates to account for any canvas scaling
    const scaleX = canvas.width / rect.width;
    const scaleY = canvas.height / rect.height;

    return {
        x: x * scaleX,
        y: y * scaleY
    };
}

// Function to get the canvas image as Base64
window.getCanvasImage = function () {
    return canvas.toDataURL(); // Return the Base64 image
};

// Function to clear the canvas
window.clearCanvas = function () {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    shapes = [];
    freeformPaths = [];
    undoStack = [];
    redoStack = [];
};

// Undo the last action
window.undo = function () {
    if (undoStack.length === 0) return;
    const lastAction = undoStack.pop();
    redoStack.push({ shapes: [...shapes], freeformPaths: [...freeformPaths] });
    shapes = lastAction.shapes;
    freeformPaths = lastAction.freeformPaths;
    redrawCanvas();
};

// Redo the last undone action
window.redo = function () {
    if (redoStack.length === 0) return;
    const nextAction = redoStack.pop();
    undoStack.push({ shapes: [...shapes], freeformPaths: [...freeformPaths] });
    shapes = nextAction.shapes;
    freeformPaths = nextAction.freeformPaths;
    redrawCanvas();
};
