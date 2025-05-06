function confirmDelete() { 
    console.log("clicked!")
    if( ! confirm("Do you really want to remove this book?") ){
        e.preventDefault();
    } else {
        ///
    }
}