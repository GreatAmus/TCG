<?php
    //Creates a new entry in `decks` table and returns its deckID

    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve deck fields from POST
    $userID = $_POST["userID"];
    $name = $_POST["name"];

    //Query database to create new deck in decks table
    $createDeckQuery = "INSERT INTO decks (userID, name) VALUES ('" . $userID . "', '" . $name . "');";
    $createDeck = mysqli_query($connection, $createDeckQuery) or die (mysqli_error($connection));

    //Query database for newly created deckID
    $getDeckIDQuery = "SELECT MAX(deckID) FROM decks;";
    $getDeckID = mysqli_query($connection, $getDeckIDQuery) or die("Get DeckID query failed.");

    $result = mysqli_fetch_array($getDeckID);

    echo "$result[0]";
?>