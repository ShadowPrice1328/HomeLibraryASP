document.getElementById('CoverImage').addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (file) {
        // Локальний попередній перегляд
        const reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('coverPreview').src = e.target.result;
        };
        reader.readAsDataURL(file);

        // Завантаження на сервер
        const formData = new FormData();
        formData.append('file', file);

        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        formData.append('__RequestVerificationToken', token);

        fetch('/Add?handler=UploadCover', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                document.querySelector('input[name="Book.Image"]').value = data.filePath;
            } else {
                alert(data.message || 'An error occurred while uploading the file.');
            }
        })
        .catch(error => {
            console.error('Error uploading file:', error);
            alert('An error occurred while uploading the file.');
        });
    }
});
