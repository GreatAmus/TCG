<?php
    //Register new user's in the database.

    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve user input username and password from POST request
    $username = $_POST["username"];
    $hashed_password = password_hash($_POST["password"], PASSWORD_DEFAULT);

    //Query database for username
    $checkUsernameQuery = $connection->prepare("SELECT username FROM users WHERE username= ?");
    $checkUsernameQuery->bind_param("s", $username);
    $checkUsernameQuery->execute();
    $result = $checkUsernameQuery->get_result();
    $checkUsernameQuery->close();

    //Make sure username is not already taken by another user
    if ($result->num_rows > 0) {
        echo "Username already exists.";
        exit();
    }

    //Query database to create new user in users table
    $createUserQuery = $connection->prepare("INSERT INTO users (username, password) VALUES (?, ?)");
    $createUserQuery->bind_param("ss", $username, $hashed_password);
    $createUserQuery->execute();
    $createUserQuery->close();

    echo("Success.");
?>