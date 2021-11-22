<?php
    //Get deck data from the database based on given deckID and userID. Returns JSON array of each cards info.

    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve fields from POST
    $userID = $_POST["userID"];
    $deckID = $_POST["deckID"];

    //If no current deckID, get ALL user's cards
    if ($deckID == -1) {
        $getCardsQuery = $connection->prepare(
            "SELECT
                cardID,
                cardName,
                description,
                portrait,
                summon,
                elementString,
                cost,
                life,
                attack,
                defense,
                cardMax,
                spellValue,
                spellType
            FROM cards WHERE userID = ?"
        );

        $getCardsQuery->bind_param("i", $userID);
        $getCardsQuery->execute();
        
    } else {
        //Query database to get ALL cards owned by userID and cardCount assigned to deckID (for edit of decks)
        $getCardsQuery = $connection->prepare(
            "SELECT
                cards.*,
                cardInDeck.cardCount
            FROM cards
            LEFT JOIN cardInDeck
                ON cards.cardID = cardInDeck.cardID AND deckID = ?
            WHERE userID = ?"
        );

        $getCardsQuery->bind_param("ii", $deckID, $userID);
        $getCardsQuery->execute();
    }

    $result = $getCardsQuery->get_result();

    $array = array();
    while($row = $result->fetch_array(MYSQLI_ASSOC)) {
        $array[] = $row;
    }

    $getCardsQuery->close();

    echo json_encode($array);

    mysqli_close($connection);
    
?>

            
            
