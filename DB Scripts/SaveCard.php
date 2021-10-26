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
    $cardName = $_POST["cardName"];
    $description = $_POST["description"];
    //$imagePath = $_POST["imagePath"];
    $imagePath = "";
    $element = $_POST["element"];
    $cost = $_POST["cost"];
    $summon = $_POST["summon"];
    $cardMax = $_POST["cardMax"];

    if ($summon) {
        $life = $_POST["life"];
        $attack = $_POST["attack"];
        $defense = $_POST["defense"];

        //Query database to create new card in cards table
        $createCardQuery = "INSERT INTO cards (cardName, description, imagePath, element, cost, summon, life, attack, defense, cardMax) VALUES ('" . $cardName . "', '" . $description . "', '" . $imagePath . "', '" . $element . "', '" . $cost . "', '" . $summon . "', '" . $life . "', '" . $attack . "', '" . $defense . "', '" . $cardMax . "');";
        mysqli_query($connection, $createCardQuery) or die ("Create card query failed.");
    }

    echo("Success.");
?>