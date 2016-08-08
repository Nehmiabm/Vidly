$(document)
    .ready(function() {
        var movieTbl = $("#movies")
            .DataTable({
                ajax: {
                    url: "/api/movies",
                    dataSrc: ""
                },
                columns: [
                    {
                        data: "name",
                        render: function(data, type, movie) {
                            return "<a href='/Movie/Edit/" + movie.movieId + "'>" + movie.name + "</a>";
                        }
                    },
                    {
                        data: "genre.name",
                        render: function(data, type, movie) {
                            return movie.genre.name;
                        }
                    },
                    {
                        data: "movieId",
                        render: function(data, type, movie) {
                            return "<button class='btn-link js-delete-movie' data-movie-id=" + data + ">Delete</button>";
                        }
                    }
                ]
            });
        //delete event
        $("#movies")
            .on("click",
                ".js-delete-movie",
                function() {
                    var btnDelete = $(this);
                    bootbox.confirm("Are you sure you want to delete the movie?",
                        function(result) {
                            if (result) {
                                $.ajax({
                                        url: "/api/movies/" + btnDelete.attr("data-movie-id"),
                                        method: "DELETE",
                                        success: function() {
                                            movieTbl.row(btnDelete.parents('tr')).remove().draw();
                                        }

                                    }
                                );
                            }
                        });

                });
    });