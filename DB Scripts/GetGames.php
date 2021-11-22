<?php
    //Get game template information for given userID. INCOMPLETE.

    require('Config.php');

    $connection = mysqli_connect($SERVER_NAME, $DB_USERNAME, $DB_PASSWORD, $DB);

    if (mysqli_connect_errno()) {
        echo "Connection to database failed.";
        exit();
    }

    //Receieve userID field from POST
    $userID = $_POST["userID"];

    $getGamesQuery = $connection->prepare("SELECT gameName, startingLife, timeLimit FROM games WHERE userID= ?");
    $getGamesQuery->bind_param("i", $userID);
    $getGamesQuery->execute();
    $result = $getGamesQuery->get_result();

    $array = array();
    while($row = $result->fetch_array(MYSQLI_ASSOC)) {
        $array[] = $row;
    }

    $getGamesQuery->close();

    echo json_encode($array);

    mysqli_close($connection);
?>