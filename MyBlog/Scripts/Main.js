//loading the Dom into jQuery
$(document).ready(function() {
    //here is where we put our code
    //selecting anything with the class of 
    //'likes', when it is clicked,
    //run a function
    $('.likes').on('click', function () {
        //When we click run this code
        //getting the id from data-id
        var id = $(this).data('id');
        //put our likes div into a variable
        var likesDiv = $(this);
        //MAKE A REQUEST TO ADD A LIKE
        $.get('/Home/Like/' + id, function (data) {
            //replace the html of the like
            //that was clicked, with the string
            //returned from our GET
            likesDiv.html(data);
        });

    });
    //adding a comment, bind a submit event to the addComment form
    $('.addComment form').on('submit', function (event) {
        alert('working');
        //stop the form from submitting normally
        event.preventDefault();
        //put this (the form into a variable
        var theForm = $(this);
        //do the Ajax Post, use the serialize
        //Command to make a string of data
        $.post('/home/addComment', $(this).serialize(), function (data) {
            //display contents of data
            //in an alert box
            theForm.parent().prepend(data);

        });//closes the $.post()
    });//closes the on submit()
 });//closes document.ready()