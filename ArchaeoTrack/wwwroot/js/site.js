window.downloadImage = function (imageUrl, filename) {
    const link = document.createElement('a');
    link.href = imageUrl;
    link.download = filename;

    // For cross-browser compatibility
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
