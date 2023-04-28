function BookDownload(fileName, fileUrl) {

    alert();
    // Create a new anchor element to trigger the download
    const downloadLink = document.createElement("a");

    // Set the href and download attributes to trigger the download
    downloadLink.href = fileUrl;
    downloadLink.download = fileName;

    // Trigger the click event on the download link
    downloadLink.click();
}