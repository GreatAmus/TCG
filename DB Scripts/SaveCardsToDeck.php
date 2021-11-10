<?php
    //Creates a new entry in `cardInDeck` table

    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve cardInDeck fields from POST
    $deckID = $_POST["deckID"];
    $cardID = $_POST["cardID"];
    $cardCount = $_POST["cardCount"];

    //Query database to create new card/deck association in table
    $createAssociationQuery = "INSERT INTO cardInDeck (deckID, cardID, cardCount) VALUES ('" . $deckID . "', '" . $cardID . "', '" . $cardCount . "');";
    $createAssociation = mysqli_query($connection, $createAssociationQuery) or die (mysqli_error($connection));

    echo "Success.";
?>