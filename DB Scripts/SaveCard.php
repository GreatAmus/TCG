<?php
    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve card fields from POST
    $userID = $_POST["userID"];
    $cardID = $_POST["cardID"];
    $cardName = $_POST["cardName"];
    $description = $_POST["description"];
    $portrait = $_POST["portrait"];
    $elementString = $_POST["elementString"];
    $cost = $_POST["cost"];
    $summon = $_POST["summon"];
    $cardMax = $_POST["cardMax"];

    //If the card is a summon
    if ($summon) {
        $life = $_POST["life"];
        $attack = $_POST["attack"];
        $defense = $_POST["defense"];

        // If card does not exist in db, insert it
        if ($cardID == -1) {
            //Query database to create new summon card in cards table
            $createSummonCardQuery = "INSERT INTO cards (userID, cardName, description, portrait, elementString, cost, summon, life, attack, defense, cardMax) VALUES ('" . $userID . "', '" . $cardName . "', '" . $description . "', '" . $portrait . "', '" . $elementString . "', '" . $cost . "', '" . $summon . "', '" . $life . "', '" . $attack . "', '" . $defense . "', '" . $cardMax . "');";
            mysqli_query($connection, $createSummonCardQuery) or die ("Create summon card query failed.");
        } else { //Card already exists in db, update it
            $updateSummonCardQuery = "UPDATE cards SET cardName = '$cardName', description = '$description', portrait = '$portrait', elementString = '$elementString', cost = '$cost', summon = '$summon', life = '$life', attack = '$attack', defense = '$defense', cardMax = '$cardMax', spellValue = NULL, spellType = NULL WHERE cardID = '$cardID';";
            mysqli_query($connection, $updateSummonCardQuery) or die (mysqli_error($connection));
        }
    } else { //The card is a spell
        $spellValue = $_POST["spellValue"];
        $spellType = $_POST["spellType"];

        //If card does not exist in db, insert it
        if ($cardID == -1) {
            //Query database to create new spell card in cards table
            $createSpellCardQuery = "INSERT INTO cards (userID, cardName, description, portrait, elementString, cost, summon, spellValue, spellType, cardMax) VALUES ('" . $userID . "', '" . $cardName . "', '" . $description . "', '" . $portrait . "', '" . $elementString . "', '" . $cost . "', '" . $summon . "', '" . $spellValue . "', '" . $spellType . "', '" . $cardMax . "');";
            mysqli_query($connection, $createSpellCardQuery) or die (mysqli_error($connection));
        } else { //Card already exists in db, update it
            $updateSpellCardQuery = "UPDATE cards SET cardName = '$cardName', description = '$description', portrait = '$portrait', elementString = '$elementString', cost = '$cost', summon = '$summon', spellValue = '$spellValue', spellType = '$spellType', cardMax = '$cardMax', life = NULL, attack = NULL, defense = NULL WHERE cardID = '$cardID';";
            mysqli_query($connection, $updateSpellCardQuery) or die (mysqli_error($connection));
        }
    }

    echo("Success.");
?>