<?php
    //Checks if correct username/password has been entered. Returns error string if incorrect. Returns userID if correct.

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
    $checkUsernameQuery = $connection->prepare("SELECT userID, password FROM users WHERE username= ?");
    $checkUsernameQuery->bind_param("s", $username);
    $checkUsernameQuery->execute();
    $result = $checkUsernameQuery->get_result();
    $checkUsernameQuery->close();

    //Make sure username exists in db
    if ($result->num_rows != 1) {
        echo "Error: Username does not exist.";
        exit();
    }


    //$queryInfo = mysqli_fetch_assoc($checkUsername);
    $row = $result->fetch_array(MYSQLI_ASSOC);
    $passwordHash = $row["password"];
    $userID = $row["userID"];

    if (password_verify($password, $passwordHash)) {
    	echo "$userID";
    } else {
    	echo "Error: Invalid password.";
    	exit();
    }
?>