<?php
    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve userID field from POST
    $userID = $_POST["userID"];

    //Query database to get games owned by userID
    $getDecksQuery = "SELECT deckID, userID, name FROM decks WHERE userID = '$userID';";
    $getDecks = mysqli_query($connection, $getDecksQuery) or die ("Get decks query failed.");

    $array = array();
    while($row = mysqli_fetch_assoc($getDecks)) {
        $array[] = $row;
    } 

    echo json_encode($array);

    mysqli_close($connection);
?>