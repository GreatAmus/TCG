<?php
    //Save cards to the database based on given cardID and userID. If card does not exist already it is created, if card exists it is updated.

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

        $spellValue = NULL;
        $spellType = NULL;

        // If summon card does not exist in db, insert it
        if ($cardID == -1) {
            $createSummonCardQuery = $connection->prepare(
                "INSERT INTO cards (
                    userID,
                    cardName,
                    description,
                    portrait,
                    elementString,
                    cost,
                    summon,
                    life,
                    attack,
                    defense,
                    cardMax)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            );

            $createSummonCardQuery->bind_param(
                "issssiiiiii",
                $userID,
                $cardName,
                $description,
                $portrait,
                $elementString,
                $cost,
                $summon,
                $life,
                $attack,
                $defense,
                $cardMax
            );
            $createSummonCardQuery->execute();
            $createSummonCardQuery->close();

            $cardID = getNewestCardID($connection);


        } else { //Summon card already exists in db, update it
            $updateSummonCardQuery = $connection->prepare(
                "UPDATE cards
                SET cardName = ?,
                    description = ?,
                    portrait = ?,
                    elementString = ?,
                    cost = ?,
                    summon = ?,
                    life = ?,
                    attack = ?,
                    defense = ?,
                    cardMax = ?,
                    spellValue = ?,
                    spellType = ?
                WHERE cardID = ?"
            );

            $updateSummonCardQuery->bind_param(
                "ssssiiiiiiiii",
                $cardName,
                $description,
                $portrait,
                $elementString,
                $cost,
                $summon,
                $life,
                $attack,
                $defense,
                $cardMax,
                $spellValue,
                $spellType,
                $cardID
            );

            $updateSummonCardQuery->execute();
            $updateSummonCardQuery->close();
        }
    } else { //The card is a spell
        $life = NULL;
        $attack = NULL;
        $defense = NULL;
        $spellValue = $_POST["spellValue"];
        $spellType = $_POST["spellType"];

        //If spell card does not exist in db, insert it
        if ($cardID == -1) {
            $createSpellCardQuery = $connection->prepare(
                "INSERT INTO cards (
                    userID,
                    cardName,
                    description,
                    portrait,
                    elementString,
                    cost,
                    summon,
                    spellValue,
                    spellType,
                    cardMax)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            );

            $createSpellCardQuery->bind_param(
                "issssiiiii",
                $userID,
                $cardName,
                $description,
                $portrait,
                $elementString,
                $cost,
                $summon,
                $spellValue,
                $spellType,
                $cardMax
            );

            $createSpellCardQuery->execute();
            $createSpellCardQuery->close();

            $cardID = getNewestCardID($connection);

        } else { //Card already exists in db, update it
            $updateSpellCardQuery = $connection->prepare(
                "UPDATE cards
                SET cardName = ?,
                    description = ?,
                    portrait = ?,
                    elementString = ?,
                    cost = ?,
                    summon = ?,
                    spellValue = ?,
                    spellType = ?,
                    cardMax = ?,
                    life = ?,5
                    attack = ?,
                    defense = ?
                WHERE cardID = ?"
            );

            $updateSpellCardQuery->bind_param(
                "ssssiiiiiiiii",
                $cardName,
                $description,
                $portrait,
                $elementString,
                $cost,
                $summon,
                $spellValue,
                $spellType,
                $cardMax,
                $life,
                $attack,
                $defense,
                $cardID
            );

            $updateSpellCardQuery->execute();
            $updateSpellCardQuery->close();
        }
    }

    echo($cardID);

    mysqli_close($connection);


    function getNewestCardID($conn) {
        //Query database for newly created cardID
        $getCardIDQuery = $conn->prepare("SELECT MAX(cardID) FROM cards");
        $getCardIDQuery->execute();
        $result = $getCardIDQuery->get_result();
        $getCardIDQuery->close();
            
        return ($result->fetch_array()[0]);
    }
?>