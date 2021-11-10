<?php
    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve userID field from POST
    $userID = $_POST["userID"];
    $deckID = $_POST["deckID"];

    //If no current deckID, get all users cards with cardCount of 0
    if ($deckID == -1) {
        //Query database to get ALL cards owned by userID
        $getCardsQuery = "SELECT cardID, cardName, description, portrait, summon, elementString, cost, life, attack, defense, cardMax, spellValue, spellType FROM cards WHERE userID = '$userID';";
        $getCards = mysqli_query($connection, $getCardsQuery) or die (mysqli_error($connection));
    } else {
        //Query database to get cards owned by userID and assigned to deckID, for edit deck
        $getCardsQuery = "
            SELECT
                cards.*,
                cardInDeck.cardCount
            FROM cards
            LEFT JOIN cardInDeck
                ON cards.cardID = cardInDeck.cardID AND deckID = '$deckID'
            WHERE userID = '$userID'
        ";
        $getCards = mysqli_query($connection, $getCardsQuery) or die (mysqli_error($connection));
    }

    $array = array();
    while($row = mysqli_fetch_assoc($getCards)) {
        $array[] = $row;
    } 

    echo json_encode($array);

    mysqli_close($connection);

    /**THIS GETS ONLY THE CARDS IN THE DECK ASSIGNED TO THE USER
            SELECT
                cards.*,
                cardInDeck.cardCount,
            FROM cards
            JOIN cardInDeck USING (cardID)
            WHERE userID = '$userID' AND deckID = '$deckID'
    */

?>

            
            
