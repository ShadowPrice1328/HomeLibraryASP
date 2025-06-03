document.querySelector('form').addEventListener('submit', async function (e) {
    e.preventDefault(); // Блокуємо стандартне надсилання

    const form = e.target;
    const fileInput = document.getElementById('CoverImage');
    const file = fileInput.files[0];

    const title = document.getElementById('Title').value.trim();
    const description = document.getElementById('Description').value.trim();
    const year = document.getElementById('Year').value.trim();
    const source = document.getElementById('Source').value.trim();
    const authors = document.getElementById('Authors').value.trim();
    const genres = document.getElementById('Genres').value.trim();

    const isEditPage = window.location.pathname.toLowerCase().includes('/edit');

    if (!title || !description || !year || !source || !authors || !genres || (!file && !isEditPage)) {
        alert('Please fill in all fields' + (isEditPage ? '.' : ' and select a cover image.'));
        return;
    }

    try {
        if (file) {
            // Завантаження зображення
            const formData = new FormData();
            formData.append('file', file);

            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            formData.append('__RequestVerificationToken', token);

            const response = await fetch('/Add?handler=UploadCover', {
                method: 'POST',
                body: formData
            });

            const data = await response.json();

            if (!data.success) {
                alert(data.message || 'Error uploading image.');
                return;
            }

            // Записуємо шлях до зображення в приховане поле
            document.querySelector('input[name="Book.Image"]').value = data.filePath;
        }

        // Надсилання форми
        form.submit();

    } catch (error) {
        console.error('Upload error:', error);
        alert('Error uploading image.');
    }
});
