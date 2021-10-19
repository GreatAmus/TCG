<?php
	require("Config.php");

	$connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve user input username and password from POST request
    $username = $_POST["username"];
    $password = $_POST["password"];

    //Query database for username
    $checkUsernameQuery = "SELECT username, password FROM users WHERE username='" . $username . "';";
    $checkUsername = mysqli_query($connection, $checkUsernameQuery) or die("Check username query failed.");

    //Make sure username exists in db
    if (mysqli_num_rows($checkUsername) != 1) {
        echo "Username does not exist.";
        exit();
    }

    $queryInfo = mysqli_fetch_assoc($checkUsername);
    $passwordHash = $queryInfo["password"];

    if (password_verify($password, $passwordHash)) {
    	echo "Success.";
    } else {
    	echo "Invalid password.";
    	exit();
    }
?>