<?php

$con = mysqli_connect('localhost','root','root','practice');

//Error Codes
//1 - Database Connection Error
//2 - Leaderboard Table Query Failed
//3 - Leaderboard Table Empty

if(mysqli_connect_errno()){
    echo("1");
    exit();
}

//Unity Form
$appPassword = $_POST["apppassword"];

//If request is not sent from app exit
if($appPassword != "thisisplaceholderpassword"){
    exit();
}

$leaderboardTableQuery = "SELECT username, score FROM players ORDER BY score DESC;";
$leaderboardTableQueryCheck = mysqli_query($con, $leaderboardTableQuery) or die("2");

if($leaderboardTableQueryCheck -> num_rows > 0){
    $json_array = array();
    while($row = mysqli_fetch_assoc($leaderboardTableQueryCheck)){
        $json_array[] = $row;
    }
    echo json_encode($json_array);
}
else{
    echo("3");
}

$con->close();

?>