function confirmDelete(event) {
    if (!confirm("Do you really want to remove this book?")) {
        console.log("don't delete bitte!! =(");
        event.preventDefault();
        return false;
    }
    return true;
}