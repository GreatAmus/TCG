<?php
    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve card fields from POST
    //$deckID = $_POST["deckID"];
    $deckID = "";
    $name = $_POST["name"];
    $description = $_POST["description"];
    //$imagePath = $_POST["imagePath"];
    $imagePath = "";
    $element = $_POST["element"];
    $cost = $_POST["cost"];
    $isSummon = $_POST["isSummon"];
    $max = $_POST["max"];

    if ($isSummon) {
        $life = $_POST["life"];
        $attack = $_POST["attack"];
        $defense = $_POST["defense"];

        //Query database to create new card in cards table
        $createCardQuery = "INSERT INTO cards (name, description, imagePath, element, cost, isSummon, life, attack, defense, max) VALUES ('" . $name . "', '" . $description . "', '" . $imagePath . "', '" . $element . "', '" . $cost . "', '" . $isSummon . "', '" . $life . "', '" . $attack . "', '" . $defense . "', '" . $max . "');";
        mysqli_query($connection, $createCardQuery) or die ("Create card query failed.");
    }

    echo("Success.");
?>