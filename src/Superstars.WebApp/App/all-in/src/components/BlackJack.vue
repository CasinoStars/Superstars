<template>
<div>


<center>
    <a class="txt"> Cartes du Dealer </a>
<div id="leespace3"></div>
    <div v-for="(i, index) of dealercards" :key="index" class="dealercards">
      <img v-if="i == '2p'" src="../img/2p.png">
      <img v-if="i == '3p'" src="../img/3p.png">
      <img v-if="i == '4p'" src="../img/4p.png">
      <img v-if="i == '5p'" src="../img/5p.png">      
      <img v-if="i == '6p'" src="../img/6p.png">
      <img v-if="i == '7p'" src="../img/7p.png">      
      <img v-if="i == '8p'" src="../img/8p.png">
      <img v-if="i == '9p'" src="../img/9p.png">      
      <img v-if="i == '10p'" src="../img/10p.png">
      <img v-if="i == '11p'" src="../img/11p.png">      
      <img v-if="i == '12p'" src="../img/12p.png">
      <img v-if="i == '13p'" src="../img/13p.png">
      <img v-if="i == '14p'" src="../img/1p.png">

      <img v-if="i == '2h'" src="../img/2h.png">
      <img v-if="i == '3h'" src="../img/3h.png">
      <img v-if="i == '4h'" src="../img/4h.png">
      <img v-if="i == '5h'" src="../img/5h.png">      
      <img v-if="i == '6h'" src="../img/6h.png">
      <img v-if="i == '7h'" src="../img/7h.png">      
      <img v-if="i == '8h'" src="../img/8h.png">
      <img v-if="i == '9h'" src="../img/9h.png">      
      <img v-if="i == '10h'" src="../img/10h.png">
      <img v-if="i == '11h'" src="../img/11h.png">      
      <img v-if="i == '12h'" src="../img/12h.png">
      <img v-if="i == '13h'" src="../img/13h.png">
      <img v-if="i == '14h'" src="../img/1h.png">

      <img v-if="i == '2t'" src="../img/2t.png">
      <img v-if="i == '3t'" src="../img/3t.png">
      <img v-if="i == '4t'" src="../img/4t.png">
      <img v-if="i == '5t'" src="../img/5t.png">      
      <img v-if="i == '6t'" src="../img/6t.png">
      <img v-if="i == '7t'" src="../img/7t.png">      
      <img v-if="i == '8t'" src="../img/8t.png">
      <img v-if="i == '9t'" src="../img/9t.png">      
      <img v-if="i == '10t'" src="../img/10t.png">
      <img v-if="i == '11t'" src="../img/11t.png">      
      <img v-if="i == '12t'" src="../img/12t.png">
      <img v-if="i == '13t'" src="../img/13t.png">
      <img v-if="i == '14t'" src="../img/1t.png">

      <img v-if="i == '2c'" src="../img/2c.png">
      <img v-if="i == '3c'" src="../img/3c.png">
      <img v-if="i == '4c'" src="../img/4c.png">
      <img v-if="i == '5c'" src="../img/5c.png">      
      <img v-if="i == '6c'" src="../img/6c.png">
      <img v-if="i == '7c'" src="../img/7c.png">      
      <img v-if="i == '8c'" src="../img/8c.png">
      <img v-if="i == '9c'" src="../img/9c.png">      
      <img v-if="i == '10c'" src="../img/10c.png">
      <img v-if="i == '11c'" src="../img/11c.png">      
      <img v-if="i == '12c'" src="../img/12c.png">
      <img v-if="i == '13c'" src="../img/13c.png">
      <img v-if="i == '14c'" src="../img/1c.png">

    </div>
</center>



<div id="tocenter">

<div id="infos">
    <a class="txt"> Valeur de votre main : {{handvalue}} </a> <br>
    <a class="txt"> Valeur de la main du dealer : {{dealerhandvalue}} </a> <br>
    <a v-if="!iaturn && !gameend" class="txt"> C'est à votre tour de jouer </a> <br>
    <a v-if="iaturn && !gameend" class="txt"> Le dealer est entrain de jouer </a> <br>
    <a v-if="gameend" class="txt"> <strong> {{winnerlooser}} </strong> </a> <br>
</div>

<div id="wait" >
<div v-if="!gameend && dealerplaying" class="lds-css ng-scope">
  <div style="width:100%;height:100%" class="lds-eclipse">
    <div>
      </div>
      </div>
      </div>
</div>

<div id="deck1">
    <img src="../img/back.png" id="deck"/>
</div>

</div>

    <center>
    <a class="txt"> Votre main </a>
<div id="leespace3"></div>

    <div v-for="(i, index) of playercards" :key="index" class="playercards">
      <img v-if="i == '2p'" src="../img/2p.png">
      <img v-if="i == '3p'" src="../img/3p.png">
      <img v-if="i == '4p'" src="../img/4p.png">
      <img v-if="i == '5p'" src="../img/5p.png">      
      <img v-if="i == '6p'" src="../img/6p.png">
      <img v-if="i == '7p'" src="../img/7p.png">      
      <img v-if="i == '8p'" src="../img/8p.png">
      <img v-if="i == '9p'" src="../img/9p.png">      
      <img v-if="i == '10p'" src="../img/10p.png">
      <img v-if="i == '11p'" src="../img/11p.png">      
      <img v-if="i == '12p'" src="../img/12p.png">
      <img v-if="i == '13p'" src="../img/13p.png">
      <img v-if="i == '14p'" src="../img/1p.png">

      <img v-if="i == '2h'" src="../img/2h.png">
      <img v-if="i == '3h'" src="../img/3h.png">
      <img v-if="i == '4h'" src="../img/4h.png">
      <img v-if="i == '5h'" src="../img/5h.png">      
      <img v-if="i == '6h'" src="../img/6h.png">
      <img v-if="i == '7h'" src="../img/7h.png">      
      <img v-if="i == '8h'" src="../img/8h.png">
      <img v-if="i == '9h'" src="../img/9h.png">      
      <img v-if="i == '10h'" src="../img/10h.png">
      <img v-if="i == '11h'" src="../img/11h.png">      
      <img v-if="i == '12h'" src="../img/12h.png">
      <img v-if="i == '13h'" src="../img/13h.png">
      <img v-if="i == '14h'" src="../img/1h.png">

      <img v-if="i == '2t'" src="../img/2t.png">
      <img v-if="i == '3t'" src="../img/3t.png">
      <img v-if="i == '4t'" src="../img/4t.png">
      <img v-if="i == '5t'" src="../img/5t.png">      
      <img v-if="i == '6t'" src="../img/6t.png">
      <img v-if="i == '7t'" src="../img/7t.png">      
      <img v-if="i == '8t'" src="../img/8t.png">
      <img v-if="i == '9t'" src="../img/9t.png">      
      <img v-if="i == '10t'" src="../img/10t.png">
      <img v-if="i == '11t'" src="../img/11t.png">      
      <img v-if="i == '12t'" src="../img/12t.png">
      <img v-if="i == '13t'" src="../img/13t.png">
      <img v-if="i == '14t'" src="../img/1t.png">

      <img v-if="i == '2c'" src="../img/2c.png">
      <img v-if="i == '3c'" src="../img/3c.png">
      <img v-if="i == '4c'" src="../img/4c.png">
      <img v-if="i == '5c'" src="../img/5c.png">      
      <img v-if="i == '6c'" src="../img/6c.png">
      <img v-if="i == '7c'" src="../img/7c.png">      
      <img v-if="i == '8c'" src="../img/8c.png">
      <img v-if="i == '9c'" src="../img/9c.png">      
      <img v-if="i == '10c'" src="../img/10c.png">
      <img v-if="i == '11c'" src="../img/11c.png">      
      <img v-if="i == '12c'" src="../img/12c.png">
      <img v-if="i == '13c'" src="../img/13c.png">
      <img v-if="i == '14c'" src="../img/1c.png">

    </div>
    </center>
<div id="leespace2"></div>
   <form @submit="hit($event)">
   <div style="text-align:center;"><button type="submit" value="hit" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false && gameend == false ">HIT</button></div>
   </form>
<div id="leespace"></div>
   <form @submit="stand($event)">
   <div style="text-align:center;"><button type="submit" value="stand" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false && gameend == false">STAND</button></div>
   </form>

   <!-- <form @submit="split($event)">
   <div style="text-align:center;"><button type="submit" value="split" class="btn btn-outline-secondary btn-lg" v-if="hasplitplayer == false && cansplitplayer && iaturn == false && gameend == false && handvalue < 21">SPLIT</button></div>
   </form> -->

   <!-- <form>
   <div><button type="button" class="btn btn-lg btn-primary" disabled>Primary button</button></div>
   <div><button type="button" class="btn btn-secondary btn-lg" disabled>Button</button></div>
   </form> -->

   <form @submit="playdealer($event)">
   <div style="text-align:center;"><button  type="submit" value="playdealer" class="btn btn-outline-secondary btn-lg" v-if="dealerhandvalue < 21 && iaturn == true && gameend == false">PLAY AI</button></div>
   </form>
<!-- v-on:click="showait()" -->

    <router-link to="/play">
      <br><div style="text-align:center;"><button  style="text-align:center;" class="btn btn-dark" v-if="gameend == true">QUITTER</button></div>
    </router-link>
   </div>
</template>

<script>
import { mapActions } from 'vuex';
import BlackJackApiService from '../services/BlackJackApiService';
import Vue from 'vue';
import GameApiService from '../services/GameApiService';


export default {
    data() {
        return {
            playercards: [],
            //playersecondcards: [],
            dealercards: [],
            handvalue: 0,
            //secondhandvalue: 0,
            dealerhandvalue: 0,
            iaturn : false,
            dealerplaying: false,
            gameend: false,
            winnerlooser: '',
            playerwin: false,
            // cansplitplayer: false,
            //  hasplitplayer: false,
            // playingsecondhand: false,
            nbturn: 0,
            nbhit: 0,
        }
    },
    async created(){
    // this.HasPlayerSplit();
    },
  async mounted() {
    this.nbturn = await this.executeAsyncRequest(() => BlackJackApiService.GetTurn());
    await this.refreshCards();
    await this.refreshHandValue();
    this.CheckWinner();
  },

    methods: {
        ...mapActions(['executeAsyncRequest']),


    async hit(e) {
        // if(this.playingsecondhand == false) {
        await this.executeAsyncRequest(() => BlackJackApiService.HitPlayer());
        // } else {
        //     await this.executeAsyncRequest(() => BlackJackApiService.HitPlayerSecondCards());
        // }
        if(this.handvalue > 21) {
            this.gameend = true;
        }
    },

     async stand(e) {
        e.preventDefault();
        // this.HasPlayerSplit();
        // if(this.hasplitplayer == true && this.playingsecondhand == false){
        //     this.iaturn = false;
        //     this.playingsecondhand = true;
        // } else {
            this.iaturn =  true;
            //this.iaturn = true;
        // }
        await this.StandandFinish();
        await this.refreshCards();
        await this.refreshHandValue();
    },

    // async split(e) {
    //     e.preventDefault();
    //     console.log("before " + typeof this.cansplitplayer)
    //     if(this.cansplitplayer) {
    //     await BlackJackApiService.SplitPlayer();
    //     this.hasplitplayer = true;
    //     this.cansplitplayer = false;
    //     await this.refreshCards();
    //     await this.refreshHandValue();
    //     console.log("after" + typeof this.hasplitplayer)
    //     }
    // },

    refreshiaturn() {
        this.iaturn = BlackJackApiService.refreshAiturn();
    },

    //  HasPlayerSplit() {
    //      this.hasplitplayer = BlackJackApiService.HasSplit();
    // },

    // CanPlayerSplit() {
        // let toto = this;
        // let michel  = BlackJackApiService.CanSplitPlayer().then(function(michel){
        //     toto.cansplitplayer = michel;
        // });

    //     this.cansplitplayer = BlackJackApiService.CanSplitPlayer();
    // },

    async StandandFinish() {
        await this.executeAsyncRequest(() => BlackJackApiService.StandPlayer());
        this.CheckWinner();
    },

//  wait(ms)
// {
//     this.showait();
// var d = new Date();
// var d2 = null;
// do { d2 = new Date(); }
// while(d2-d < ms);
// },

// showait() {
//     console.log("BISMILLAH");
//     console.log( document.getElementById('wait'));
//     document.getElementById('wait').style.visibility = "visible";
// },

    async playdealer(e) {
        e.preventDefault();
        // this.wait(5000);
        document.getElementById('wait').style.visibility = "visible";
        this.dealerplaying = true;
        setTimeout(this.playdealersecond,2000);
    },

    async playdealersecond() {
        await this.executeAsyncRequest(() => BlackJackApiService.PlayAI());
        await this.refreshCards();
        await this.refreshHandValue();
        this.gameend = true;
        this.CheckWinner();
    },

     CheckWinner() {
        if(this.gameend == true || this.handvalue == this.dealerhandvalue && this.iaturn == true || this.handvalue == 21 || this.handvalue > 21 || this.dealerhandvalue == 21 || this.dealerhandvalue > 21) {
        if(this.handvalue > 21 ) {
            this.winnerlooser = 'Vous avez perdu !';
        } else if(this.dealerhandvalue > 21) {
            this.winnerlooser = 'Vous avez gagné !'
            this.playerwin = true;
        } else if(this.dealerhandvalue == 21) {
            this.winnerlooser = 'BLACKJACK! Vous avez perdu !'
        } else if(this.handvalue == 21) {
            this.winnerlooser = 'BLACKJACK ! Vous avez gagné !';
            this.playerwin = true;
        } else if(this.handvalue < this.dealerhandvalue ) {
            this.winnerlooser = 'Vous avez perdu !';
        }  else if(this.handvalue > this.dealerhandvalue) {
            this.winnerlooser = 'Vous avez gagné !';
            this.playerwin = true;
        } else if(this.handvalue == this.dealerhandvalue) {
            this.winnerlooser = "Egalité";
        }
         this.gameend = true;
         this.updateStats();
        }
    },

    async updateStats() {
        await this.executeAsyncRequest(() => GameApiService.UpdateStats('BlackJack',this.playerwin));
    },

    async refreshHandValue() {
        this.handvalue = await this.executeAsyncRequest(() => BlackJackApiService.getplayerHandValue());
        this.dealerhandvalue = await this.executeAsyncRequest(() => BlackJackApiService.getAiHandValue());

    //     if(this.hasplitplayer) {
    //       this.secondhandvalue = await this.executeAsyncRequest(() => BlackJackApiService.getplayerSecondHandValue());
    //   }
    },

    async refreshCards() {
      this.playercards = await this.executeAsyncRequest(() => BlackJackApiService.GetPlayerCards());
      this.dealercards = await this.executeAsyncRequest(() => BlackJackApiService.GetAiCards());

    //   if(this.hasplitplayer) {
    //       console.log("ET OUI");
    //       this.playersecondcards = await this.executeAsyncRequest(() => BlackJackApiService.GetSecondPlayerCards());
    //   }
    },
    }
}
</script>

<style>

/* ECLIPSE LOADER */

@keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
@-webkit-keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
.lds-eclipse {
  position: relative;
}

.lds-eclipse div {
  position: absolute;
  -webkit-animation: lds-eclipse 1s linear infinite;
  animation: lds-eclipse 1s linear infinite;
  width: 40px;
  height: 40px;
  top: 80px;
  left: 80px;
  border-radius: 50%;
  box-shadow: 0 2px 0 0 #7f8387;
  -webkit-transform-origin: 20px 21px;
  transform-origin: 20px 21px;
}
.lds-eclipse {
  width: 200px !important;
  height: 200px !important;
  -webkit-transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
  transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
  margin-left: 50%;
}

.txt {
  font-family: 'Courier New', sans-serif;
  font-variant: small-caps;
  color: rgb(0, 0, 0);
  font-size: 28px;
}

#tocenter {
    display: -webkit-inline-box;
}

#infos {
margin-top: 3%;
padding-left: 1%;
}

 #deck {
height: 215px;
width: 135px;
 }


#deck1 {
    margin-left: 680px;
}

#wait {
    margin-left: 300px;
    visibility: hidden;
}

 .playercards > img {
     height: 205px;
     width: 125px;
 }
 
 .playercards {
	display: inline-block;
	position: relative;
	
 }

 .dealercards {
	display: inline-block;
	position: relative;
 }

 .dealercards > img {
     height: 205px;
     width: 125px;

 }

#leespace {
    height: 5px;
    width: 5px;
}

#leespace2 {
    height: 20px;
    width: 20px;
}

#leespace3 {
    height: 10px;
    width: 10px;
}
</style>



