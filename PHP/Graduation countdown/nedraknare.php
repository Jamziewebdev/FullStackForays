<!DOCTYPE html>
<html lang="sv">
<head>
    <title>Examensnedräknare</title>
    <meta charset="utf-8">

    <style>
    body {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        padding: 200px;
        text-align: center;
        background-color: #D8D5DC;
    }

    #wrapper {
        padding: 20px;
        border: 1px solid black;
        border-radius: 5px;
        background-color: #E2DCEB;
    }

    h1 {
        margin: 15px;
        text-decoration: underline;
        filter: drop-shadow(5px 10px 8px #5F4485);
    }

    #daysLeftBox {
        color: #fff;
        padding: 10px;
        margin-top: 20px;
        display: inline-block;
        border-radius: 5px;
        animation: blinkingBackground 4s infinite;
        font-size: 20px;
        filter: drop-shadow(5px 10px 8px #5F4485);
    }

    @keyframes blinkingBackground {
        0%          { background-color: #10c018;}
        25%         { background-color: #1056c0;}
        50%         { background-color: #ef0a1a;}
        75%         { background-color: #254878;}
        100%        { background-color: #04a1d5;}
    }
    </style>
</head>
<body>
    <div id="wrapper">
            <h1>Countdown to graduation</h1>

            <?php
            function getTodaysDate() {
                return date("l F j, Y");
            }

            $gradDate = strtotime("16 August 2024");

            $currentDate = time();

            $difference = $gradDate - $currentDate;

            $daysLeft = (int)($difference / 86400);
            
            echo "Today is <strong>" . getTodaysDate() . "</strong><br>";
            ?>
        <div id="daysLeftBox">
        <?php
        echo "&#129395; There are only <strong>$daysLeft</strong> days left until you graduate on the <strong>16th August 2024</strong> &#129395;";
        ?>
        </div>
    </div>
</body>
</html>
