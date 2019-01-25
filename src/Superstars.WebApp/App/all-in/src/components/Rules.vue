<template>
<div class="bg">
  <br>
  <div style="text-align: center;margin-top 2%;font-family: 'Courier New', sans-serif;">
      <h1 style="font-variant: small-caps;font-size: 65px;"> <strong> Règles </strong></h1>
<center>
<div>
<br>
<button v-on:click="YamsRules()" class="btn btn bg-dark" style="font-family: 'Courier New', sans-serif; font-size: 25px; color: white; width: 180px; height: 45px;"><strong>YAMS</strong></button>
<button v-on:click="BlackJackRules()" class="btn btn bg-dark" style="font-family: 'Courier New', sans-serif; font-size: 25px; color: white; width: 180px; height: 45px;"><strong>BLACKJACK</strong></button>
<button v-on:click="CrashRules()" class="btn btn bg-dark" style="font-family: 'Courier New', sans-serif; font-size: 25px; color: white; width: 180px; height: 45px;"><strong>CRASH</strong></button>
</div>
<br>
<!-- <div class="line" style="height: 1px; background-color: black;"></div> -->
</center>
      <br>
      <h2  id="YamsTitle" style="font-variant: small-caps; font-size: 40px;"> <strong> <dt>Le Yams </dt></strong></h2>
  </div>
<br>

<div id="YamsContent">
<div  class="text">
  <p>
  Le Yahtzee (aussi orthographié Yatzee) ou Yam's est un jeu de société traditionnel de hasard raisonné. <br>
Le but est d'enchaîner les combinaisons à l'aide de cinq dés pour remporter un maximum de points. Il appartient au domaine public. <br>
Notre site web vous permet  de jouer une partie de Yam’s contre notre ordinateur.<br>
Chaque joueur lance les dés lorsque vient son tour. Son but est, à l'aide des cinq dés, de réaliser une figure (5 dés identiques (Yam’s), un brelan...). <br>
Pour réaliser cette figure il a le droit à trois jets de dés et il est à chaque jet, libre de garder tous ses dés, de tous les relancer ou seulement ceux de son choix. <br>
Le gagnant est celui qui a fait la meilleure figure. En cas d’égalité, c’est la valeur des dés qui décide du gagnant.<br>
</p>
</div>
<br><br>
<div class="text">
<dl>
<dt> <strong> Liste des figures, de la plus faible à la plus forte : </strong> </dt>
<br>
<strong>Chance</strong>
<dd>  le joueur n'a pas de figure </dd>
<br>
 <strong>Brelan </strong>
<dd>  le joueur a <strong>3 dés de même valeur</strong> </dd>
<br>
 <strong>Carré</strong>
<dd>  le joueur a <strong>4 dés de même valeur</strong> </dd>
<br>
 <strong> Full</strong>
<dd>  le joueur a <strong>3 dés de même valeur + 2 dés de même valeur </strong> : ( <img src="../img/dice3.png"><img  src="../img/dice3.png"><img  src="../img/dice3.png"><img  src="../img/dice2.png"><img  src="../img/dice2.png"> ) est un full de 3 par 2</dd>
<br>
 <strong>Petite Suite</strong>
<dd>  le joueur a <strong>5 dés qui se suivent</strong> en commencant par le <strong>1</strong>( <img src="../img/dice1.png"><img  src="../img/dice2.png"><img  src="../img/dice3.png"><img  src="../img/dice4.png"><img  src="../img/dice5.png"> )</dd>
<br>
<strong>Grande Suite</strong>
<dd>  le joueur a <strong>5 dés qui se suivent </strong>  en commencant par le <strong>2</strong>( <img src="../img/dice2.png"><img  src="../img/dice3.png"><img  src="../img/dice4.png"><img  src="../img/dice5.png"><img  src="../img/dice6.png"> )</dd>
<br>
<strong>Yam’s</strong>
<dd>  le joueur a <strong>5 dés de même valeur</strong> </dd>
</dl>
</div>
</div>


<!--  BLACKJACK -->
  <div style="text-align: center; font-family: 'Courier New', sans-serif;">
      <h2 id="bjtitle" style="font-variant: small-caps; font-size: 40px;"> <strong>Le BlackJack </strong></h2>
  </div>
<br>
  <div id="BlackJackContent">
<div class="text">
  <p>
  Le Blackjack est un jeu de cartes très populaire dans les casinos.<br>
La partie oppose le joueur contre la banque (aussi appelée le dealer). Le dealer distribue deux cartes visibles au joueur mais ne se distribue qu’une seule carte.<br> 
à chaque tour, le joueur peut choisir de  tirer une carte (ou non) pour s’approcher de la valeur 21 en essayant de ne pas la dépasser.<br>
 Une fois que vous décidez de ne plus tirer de carte, c'est au tour du dealer !<br>
Le but du joueur est de battre le dealer en obtenant un total de points supérieur à celui-ci ou en voyant ce dernier dépasser 21. <br>
Valeur des cartes : <br>
- Chaque carte numérotée de 2 à 10 a sa valeur nominale (égale au numéro sur la carte) <br>
- Les valets, les dames et les rois (les figures), ont une valeur de 10 points <br>
- L’As vaut 1 point ou 11 points, au choix du joueur <br>
</p>
</div>
</div>


<!-- CRASH -->
<div style="text-align: center; font-family: 'Courier New', sans-serif;">
      <h2 id="crashtitle" style="font-variant: small-caps; font-size: 40px;"> <strong>Le jeu du Crash </strong></h2>
  </div>
<br>
  <div id="CrashContent">
<div class="text">
  <p>
  Le jeu du Crash est un jeu qui consiste en une courbe croissante qui peut s'arrêter à n'importe quel moment !<br>
La partie consiste à placer un pari et regarder le multiplicateur monter. Vous pouvez sortir votre argent à tout moment et obtenir votre mise multipliée par la valeur du multiplicateur.<br>
Mais attention  car la courbe peut s'arrêter à n'importe quel moment et vous aurez tout perdu !
</p>
</div>
<img id="Crashgif" src="../img/CrashGIF.gif" height="700px" width="500px">
</div>


</div>
</template>

<script>
import { mapActions } from 'vuex';
import Vue from 'vue';

export default {
    
  data(){
    return {

    }
  },

      mounted(){
       //this.YamsRules();
    },

     methods: {
    ...mapActions(['executeAsyncRequest']),

     YamsRules() {  
      var element3 = document.getElementById("BlackJackContent");
      element3.style.display = "none";
      var element4 = document.getElementById("bjtitle");
      element4.style.display = "none";
      var element = document.getElementById("YamsContent");
      element.style.display = "block";
      var element2 = document.getElementById("YamsTitle");
      element2.style.display = "block";
      var element5 = document.getElementById("CrashContent");
      element5.style.display = "none";
      var element6 = document.getElementById("crashtitle");
      element6.style.display = "none";
    },

     BlackJackRules() {
      var element3 = document.getElementById("BlackJackContent");
      element3.style.display = "block";
      var element4 = document.getElementById("bjtitle");
      element4.style.display = "block";
      var element = document.getElementById("YamsContent");
      element.style.display = "none";
      var element2 = document.getElementById("YamsTitle");
      element2.style.display = "none";
      var element5 = document.getElementById("CrashContent");
      element5.style.display = "none";
      var element6 = document.getElementById("crashtitle");
      element6.style.display = "none";
    },

     CrashRules() {
      var element3 = document.getElementById("CrashContent");
      element3.style.display = "block";
      var element4 = document.getElementById("crashtitle");
      element4.style.display = "block";
      var element = document.getElementById("YamsContent");
      element.style.display = "none";
      var element2 = document.getElementById("YamsTitle");
      element2.style.display = "none";
      var element5 = document.getElementById("BlackJackContent");
      element5.style.display = "none";
      var element6 = document.getElementById("bjtitle");
      element6.style.display = "none";
    },
     } 
}
</script>


<style>

.bg p {
   text-align: justify;
   font-weight: bold;
   margin-right: 4%;
   margin-left: 2%;
   font-size: 34px;
}

.text {
  text-align: center;
  font-size: 42px;
  font-family: 'Courier New', sans-serif;
  font-variant: small-caps;
  color: black;

}

dt {
  text-decoration: underline;
}

img {
  width: 38px;
  height: 38px;
}

#Crashgif {
width: 700px;
height: 350px;
display: block;
margin-left: auto;
margin-right: auto;
margin-top: 2%;
margin-bottom: 3%;
border-radius: 2%;
}

#BlackJackContent {
  display: none;
}

#bjtitle {
  display: none;
}

#CrashContent {
  display: none;
}

#crashtitle {
  display: none;
}

#YamsContent {
  display: block;
}

#Yamstitle {
  display: block;
}


</style>