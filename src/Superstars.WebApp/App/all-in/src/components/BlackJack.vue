<template>
  <div class="blackJack">
    <!-- MODAL FOR BET -->
    <div id="myModal" class="modal">
      <!-- Modal content -->
      <div class="modal-content">

        <div class="modal-header">
          <div style="padding-top: 2px; font-family: 'Courier New', sans-serif;" class="col-12 modal-title text-center">
            <router-link class="close" v-on:click.native="RedirectandDelete()" to="">&times;</router-link>
            <h2 v-if="realOrFake == 'real'">SOLDE DE VOTRE COMPTE BTC: {{BTCMoney.toLocaleString('en')}} <i class="fa fa-btc"
                style="font-size: 1.5rem;"></i></h2>
            <h2 v-else>SOLDE DE VOTRE COMPTE ALL'IN: {{fakeMoney.toLocaleString('en')}} <i class="fa fa-money" style="font-size: 1.5rem;"></i></h2>
          </div>
        </div>
        <ul class="tab-group">
          <li class="tab active" v-if="this.realOrFake == 'real'"><a v-on:click="changeBet('real')">Réel</a></li>
          <li class="tab" v-else><a v-on:click="changeBet('real')">Réel</a></li>
          <li class="tab active" v-if="this.realOrFake == 'fake'"><a v-on:click="changeBet('real')">Virtuel</a></li>
          <li class="tab" v-else><a v-on:click="changeBet('fake')">Virtuel</a></li>
        </ul>
        <form @submit="bet($event)">
          <div class="modal-body">
            <h4 style="color: white; font-family: 'Courier New', sans-serif;" v-if="realOrFake == 'real'">SAISISSEZ
              VOTRE MISE EN BTC <span class="req">*</span></h4>
            <h4 style="color: white; font-family: 'Courier New', sans-serif;" v-else>SAISISSEZ VOTRE MISE EN ALL'IN
              <span class="req">*</span></h4>
            <input style="margin-top: 10px; margin-bottom: 1%;" v-if="realOrFake == 'real'" type="decimal" v-model="trueBet"
              required />
            <input style="margin-top: 10px; margin-bottom: 1%;" v-else type="number" v-model="fakeBet" required />

            <div class="alert alert-danger" style="opacity: 0.8; font-weight: bold; font-family: 'Courier New', sans-serif; text-transform: uppercase; margin-top: 1%; margin-bottom: 0.5%; text-align: center; font-family: 'Courier New', sans-serif;"
              v-if="errors.length > 0">
              <div style="opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
            </div>
          </div>

          <div class="modal-footer">
            <div class="col-12 modal-title text-center">
              <router-link class="btn btn-secondary" v-on:click.native="RedirectandDelete()" to="">Annuler</router-link>
              <button type="submit" class="btn btn-light">Confirmer</button>
            </div>
          </div>
        </form>
      </div>
    </div>

    <!-- BLACKJACK TUTO -->
    <div id="tocenter">
      <div id="tutorialRectanglebj" class="bg-dark" v-if="playerBet == true && nbturn == 0 && wins == 0">
        <p id="tutorialText0"> {{tutorialp0}}</p>
        <p id="tutorialText1"> {{tutorialp1}}</p>
        <p id="tutorialText2"> {{tutorialp2}}</p>
        <p id="tutorialText3"> {{tutorialp3}}</p>
        <p id="tutorialText4"> {{tutorialp4}}</p>
        <button class="btn btn-secondary active" id="tutorialButton" v-on:click="OkTutorial()"> Ok ! </button>
      </div>
    </div>

    <!-- BLACKJACK GAME -->
    <div class="container" style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top:2%;">
      <center>
        <div class="row">
          <div class="col">
            <a class="txt">POT: {{pot.toLocaleString('en')}}<i v-if="this.realOrFake == 'real'" class="fa fa-btc" /><i
                v-else class="fa fa-money" /></a>
          </div>
        </div><br>
        <a class="txt">Valeur de la main du dealer: <strong>{{dealerhandvalue}}</strong></a>
      </center>
    </div>

    <center>
      <a class="txt">Cartes du Dealer</a><br>
      <div v-for="(i, index) of dealercards" :key="index" class="dealercards">
        <img :src="getCardImage(i)" :id="index" v-if="i != ''">
        <img src="../img/back.png" :id="index" v-else-if="i == '' && !iaturn">
      </div>
    </center>

    <div style="margin-top:3%; text-align:center; letter-spacing: 2px; font-family: 'Courier New', sans-serif;" id="Infos">
      <a v-if="!iaturn && !gameend" class="txt">C'est à votre tour de jouer</a>
      <a v-if="dealerplaying && !gameend" class="txt">Le dealer est entrain de jouer</a>
      <div id="wait">
        <div v-if="!gameend && dealerplaying" class="lds-css ng-scope">
          <div style="width:100%;height:100% margin-left:-50%;" class="lds-eclipse">
            <div></div>
          </div>
        </div>
      </div>
      <a v-if="gameend" class="txt"><strong>{{winnerlooser}}</strong></a>
    </div>

    <!-- <img src="../img/back.png" id="deck" style="float: right; margin-top:3%;"/> -->

    <div id="wait">
      <div v-if="!gameend && dealerplaying" class="lds-css ng-scope">
        <div style="width:100%;height:100%" class="lds-eclipse">
          <div></div>
        </div>
      </div>
    </div>

    <center style="margin-top:3%;">
      <a class="txt">Votre main</a><br>
      <div v-for="(i, index) of playercards" :key="index" class="playercards">
        <img :src="getCardImage(i)" :id="index" v-if="i != ''">
      </div>
    </center>

    <center>
      <div style="letter-spacing: 2px; font-family: 'Courier New', sans-serif; margin-top:2%;">
        <a class="txt">Valeur de votre main: <strong>{{handvalue}}</strong></a>
      </div>
      <form @submit="hit($event)">
        <button type="submit" value="hit" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false && gameend == false">HIT</button>
      </form>
      <form @submit="stand($event)">
        <button type="submit" value="stand" class="btn btn-outline-secondary btn-lg" v-if="handvalue < 21 && iaturn == false && gameend == false">STAND</button>
      </form>
      <form @submit="playdealer($event)">
        <button type="submit" value="playdealer" class="btn btn-outline-secondary btn-lg" v-if="dealerhandvalue < 21 && iaturn == true && gameend == false">PLAY
          AI</button>
      </form>
    </center>

    <!-- <form @submit="split($event)">
   <div style="text-align:center;"><button type="submit" value="split" class="btn btn-outline-secondary btn-lg" v-if="hasplitplayer == false && cansplitplayer && iaturn == false && gameend == false && handvalue < 21">SPLIT</button></div>
   </form> -->

    <!-- <form>
   <div><button type="button" class="btn btn-lg btn-primary" disabled>Primary button</button></div>
   <div><button type="button" class="btn btn-secondary btn-lg" disabled>Button</button></div>
   </form> -->

    <!-- v-on:click="showait()" -->

    <router-link to="/play">
      <br>
      <div style="text-align:center;"><button style="text-align:center;" class="btn btn-dark" v-if="gameend == true">QUITTER</button></div>
    </router-link>


    <div id="snackbar">{{success}} <i style="color:green" class="fa fa-check"></i></div>
  </div>
</template>

<script>
  import {
    mapActions,
    mapGetters
  } from 'vuex';
  import BlackJackApiService from '../services/BlackJackApiService';
  import UserApiService from '../services/UserApiService';
  import Vue from 'vue';
  import GameApiService from '../services/GameApiService';
  import WalletApiService from '../services/WalletApiService';


  export default {
    data() {
      return {
        playercards: [],
        dealercards: [],
        handvalue: 0,
        dealerhandvalue: 0,
        iaturn: false,
        dealerplaying: false,
        gameend: false,
        winnerlooser: '',
        playerwin: '',
        nbturn: 0,
        nbhit: 0,
        playerBet: false,
        realOrFake: 'real',
        fakeBet: 0,
        trueBet: 0,
        errors: [],
        success: '',
        pot: 0,
        tutorialp0: '',
        tutorialp1: '',
        tutorialp2: '',
        tutorialp3: '',
        tutorialp4: '',
        queryPseudo: '',
        nbSlidesTutorial: 0,
        wins: 0
      }
    },
    computed: {
      ...mapGetters(['BTCMoney']),
      ...mapGetters(['fakeMoney'])
    },

    async mounted() {
      await this.refreshCards();

      if (this.$route.query.pseudo)
        this.queryPseudo = this.$route.query.pseudo;
      else
        this.queryPseudo = UserApiService.pseudo;

      this.wins = await this.executeAsyncRequest(() => GameApiService.getWinsBlackJackPlayer(this.queryPseudo));
      console.log(this.wins);
      this.pot = await this.executeAsyncRequest(() => GameApiService.getBlackJackPot());
      this.nbturn = await this.executeAsyncRequest(() => BlackJackApiService.GetTurn());

      this.refreshiaturn();

      if (this.pot == 0 || this.pot == null) {
        this.showModal();
      } else {
        this.playerBet = true;
        await this.CheckWinner();
        await this.refreshHandValue();
      }
      this.tutorialp0 = "Bienvenue sur le BlackJack !";
      await this.CheckWinner();

    },

    methods: {
      ...mapActions(['executeAsyncRequest']),
      ...mapActions(['RefreshFakeCoins']),
      ...mapActions(['RefreshBTC']),

      OkTutorial() {
        let rectangle = document.getElementById("tutorialRectanglebj");

        this.nbSlidesTutorial = this.nbSlidesTutorial + 1;
        if (this.nbSlidesTutorial == 1) {
          document.getElementById("tutorialText0").style.opacity = 0.5;
          this.tutorialp1 = "  Vous allez devoir vous approchez le plus près possible de 21 mais sans le dépasser.  ";
        } else if (this.nbSlidesTutorial == 2) {
          document.getElementById("tutorialText1").style.opacity = 0.5;
          this.tutorialp2 =
            "  Vous pouvez à chaque tour, décider de tirer une carte ou de vous arrêter pour conserver votre valeur actuelle.  ";
        } else if (this.nbSlidesTutorial == 3) {
          document.getElementById("tutorialText2").style.opacity = 0.5;
          this.tutorialp3 = "  L'ordinateur jouera après vous en suivant ces mêmes règles.  ";
        } else if (this.nbSlidesTutorial == 4) {
          document.getElementById("tutorialText3").style.opacity = 0.5;
          this.tutorialp4 = "  Le joueur le plus proche de 21 sans le dépasser remporte la partie ! Bonne chance !  ";
        } else if (this.nbSlidesTutorial == 5) {
          rectangle.classList.toggle('fade');
        }
      },

      async RedirectandDelete() {
        await this.executeAsyncRequest(() => GameApiService.deleteGame(1));
        this.$router.push({
          path: 'play'
        });
      },

      changeBet(choice) {
        this.realOrFake = choice;
        this.errors = 0;
      },

      showModal() {
        var modal = document.getElementById('myModal');
        modal.style.display = "block";
      },

      async bet(e) {
        e.preventDefault();
        var errors = [];
        this.errors = 0;
        if (this.realOrFake === 'fake') {
          if (this.fakeBet > 10000000)
            errors.push("La mise maximum est de 10,000,000");
          else if (this.fakeBet <= 0)
            errors.push("La mise doit être supérieur à 0");
          else if (this.fakeBet > this.fakeMoney)
            errors.push("Vous n'avez pas cette somme sur votre compte");
        } else {
          if (this.trueBet > 10000000)
            errors.push("La mise maximum est de 10,000,000 bits");
          else if (this.trueBet <= 0)
            errors.push("La mise doit être supérieur à 0 bits");
          else if (this.trueBet > this.BTCMoney) {
            errors.push("Vous n'avez pas cette somme sur votre compte");
          }
        }
        this.errors = errors;
        if (errors.length == 0) {
          try {
            if (this.realOrFake === 'fake') {
              await this.executeAsyncRequest(() => GameApiService.BetFake(this.fakeBet, 1));
              await this.RefreshFakeCoins();
              this.success = 'Vous venez de parier: ' + this.fakeBet + ' All`In Coins';
            } else {
              await this.executeAsyncRequest(() => GameApiService.BetBTC(this.trueBet, 1));
              await this.RefreshBTC();
              this.success = 'Vous venez de parier: ' + this.trueBet + ' Bits';
            }
            var x = document.getElementById("snackbar");
            x.className = "show";
            setTimeout(function () {
              x.className = x.className.replace("show", "");
            }, 3000);
            var modal = document.getElementById('myModal');
            modal.style.display = "none";
            this.pot = await this.executeAsyncRequest(() => GameApiService.getBlackJackPot());
            await this.refreshCards();
            this.playerBet = true;
            await this.refreshHandValue();
          } catch (error) {}
        }
      },

      async hit(e) {
        e.preventDefault();
        await this.executeAsyncRequest(() => BlackJackApiService.HitPlayer());

        if (this.handvalue >= 21) {
          this.gameend = true;
        }
        this.nbturn = await this.executeAsyncRequest(() => BlackJackApiService.GetTurn());
        await this.refreshCards();
        await this.refreshHandValue();
        await this.CheckWinner();
      },

      async stand(e) {
        e.preventDefault();
        this.iaturn = true;
        await this.StandandFinish();
        await this.refreshCards();
        await this.refreshHandValue();
        await this.CheckWinner();
      },


      async refreshiaturn() {
        this.iaturn = await this.executeAsyncRequest(() => BlackJackApiService.refreshAiturn());
      },

      async StandandFinish() {
        await this.executeAsyncRequest(() => BlackJackApiService.StandPlayer());
        await this.CheckWinner();
      },

      async playdealer(e) {
        e.preventDefault();
        document.getElementById('wait').style.visibility = "visible";
        this.dealerplaying = true;
        await new Promise(f => setTimeout(f, 2000));
        await this.playdealersecond();
      },

      async playdealersecond() {
        await this.executeAsyncRequest(() => BlackJackApiService.PlayAI());
        await this.refreshCards();
        await this.refreshHandValue();
        this.gameend = true;
        await this.CheckWinner();
      },

      async CheckWinner() {
        if (this.gameend == true || this.handvalue == this.dealerhandvalue && this.iaturn == true || this.handvalue ==
          21 || this.handvalue > 21 || this.dealerhandvalue == 21 || this.dealerhandvalue > 21) {
          if (this.handvalue > 21) {
            this.winnerlooser = 'Vous avez perdu !';
            this.playerwin = 'AI';
          } else if (this.dealerhandvalue > 21) {
            this.winnerlooser = 'Vous avez gagné !'
            this.playerwin = 'Player';
          } else if (this.dealerhandvalue == 21) {
            this.winnerlooser = 'BLACKJACK ! Vous avez perdu !'
            this.playerwin = 'AI';
          } else if (this.handvalue == 21) {
            this.winnerlooser = 'BLACKJACK ! Vous avez gagné !';
            this.playerwin = 'Player';
          } else if (this.handvalue < this.dealerhandvalue) {
            this.winnerlooser = 'Vous avez perdu !';
            this.playerwin = 'AI';
          } else if (this.handvalue > this.dealerhandvalue) {
            this.winnerlooser = 'Vous avez gagné !';
            this.playerwin = 'Player';
          } else if (this.handvalue == this.dealerhandvalue) {
            this.winnerlooser = "Egalité";
            this.playerwin = 'Equality';
            var egalite = true;
          }
          this.gameend = true;
          if (this.playerwin == 'AI') {
            if (this.trueBet == 0)
              await this.updateStats(0, this.fakeBet);
            else
              await this.updateStats(1, this.trueBet);
          }
          if (this.playerwin == 'Player') {
            var pot = await this.executeAsyncRequest(() => GameApiService.getBlackJackPot());
            if (this.trueBet == 0) {
              await this.executeAsyncRequest(() => WalletApiService.WithdrawFakeBankRoll(pot));
              await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInFake(pot));
              await this.RefreshFakeCoins();
              await this.updateStats(0, this.fakeBet);
            } else {
              await this.executeAsyncRequest(() => WalletApiService.WithdrawBTCBankRoll(pot));
              await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInBTC(pot));
              await this.RefreshBTC();
              await this.updateStats(1, this.trueBet);
            }
          }
          if (egalite != null) {
            var pot = await this.executeAsyncRequest(() => GameApiService.getBlackJackPot());
            if (this.trueBet == 0) {
              await this.executeAsyncRequest(() => WalletApiService.WithdrawFakeBankRoll(pot / 2));
              await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInFake(pot / 2));
              await this.RefreshFakeCoins();
              await this.updateStats(0, 0);
            } else {
              await this.executeAsyncRequest(() => WalletApiService.WithdrawBTCBankRoll(pot / 2));
              await this.executeAsyncRequest(() => WalletApiService.CreditPlayerInBTC(pot / 2));
              await this.RefreshBTC();
              await this.updateStats(1, 0);
            }
            await this.executeAsyncRequest(() => GameApiService.gameEndUpdate(1, this.playerwin, this.realOrFake));
            await this.executeAsyncRequest(() => BlackJackApiService.DeleteJackAiPlayer());
            await this.executeAsyncRequest(() => GameApiService.DeleteAis(1));
            return;
          }
        }
      },

      async updateStats(moneyType, bet) {
        await this.executeAsyncRequest(() => GameApiService.UpdateStats(1, moneyType, bet, this.playerwin));
        await this.executeAsyncRequest(() => GameApiService.gameEndUpdate(1, this.playerwin, this.realOrFake));
        await this.executeAsyncRequest(() => BlackJackApiService.DeleteJackAiPlayer());
        await this.executeAsyncRequest(() => GameApiService.DeleteAis(1));
      },

      async refreshHandValue() {
        this.handvalue = await this.executeAsyncRequest(() => BlackJackApiService.getplayerHandValue());
        this.dealerhandvalue = await this.executeAsyncRequest(() => BlackJackApiService.getAiHandValue());
      },

      async refreshCards() {
        this.playercards = await this.executeAsyncRequest(() => BlackJackApiService.GetPlayerCards());
        this.dealercards = await this.executeAsyncRequest(() => BlackJackApiService.GetAiCards());
      },

      getCardImage(value) {
        let image = `back.png`;
        if (this.playerBet) {
          if (value == '14p')
            image = `1p.png`;
          else if (value == '14h')
            image = `1h.png`;
          else if (value == '14c')
            image = `1c.png`;
          else if (value == '14t')
            image = `1t.png`;
          else
            image = `${value}.png`;
        }
        return require(`../img/${image}`);
      }
    }
  }
</script>

<style lang="scss">
  $body-bg: #c1bdba;
  $form-bg: #13232f;
  $white: #ffffff;
  $main: #777c7b;
  $main-dark: darken($main, 5%);
  $gray-light: #a0b3b0;

  #tutorialRectanglebj {
    width: 95%;
    height: 80%;
    margin-left: 3%;
    margin-top: -11%;
    border-radius: 20px;
    text-align: center;
    opacity: 0.99;
    position: absolute;
    transition: opacity 1s;
    z-index: 15;
  }

  #tutorialRectanglebj.fade {
    visibility: hidden;
    opacity: 0;
    transition: visibility 0s 2s, opacity 2s linear;
  }

  .blackJack #tutorialRectanglebj>p {
    color: white;
    text-transform: uppercase;
    font-size: 28px;
    font-family: 'Courier New', sans-serif;
    text-align: center;
    position: relative;
    margin-top: 3%;
  }

  .blackJack #tutorialButton {
    text-align: center;
    text-transform: uppercase;
    font-family: 'Courier New', sans-serif;
    display: inline-block;
    font-size: 26px;
    border-radius: 3px;
    position: relative;
    margin-top: 5%;
  }

  .blackJack .tab-group {
    list-style: none;
    padding: 0;
    margin: 0 0 5px 0;

    &:after {
      content: "";
      display: table;
      clear: both;
    }

    li a {
      display: block;
      text-decoration: none;
      padding: 8px;
      background: rgba($gray-light, .25);
      color: $gray-light;
      font-size: 20px;
      float: left;
      width: 50%;
      text-align: center;
      cursor: pointer;
      transition: .5s ease;

      &:hover {
        background: $main-dark;
        color: $white;
      }
    }

    .active a {
      background: $main;
      color: $white;
    }
  }

  /* The Modal (background) */
  .blackJack .modal {
    display: none;
    /* Hidden by default */
    position: fixed;
    /* Stay in place */
    z-index: 1;
    /* Sit on top */
    padding-top: 16%;
    /* Location of the box */
    left: 0;
    top: 0;
    width: 100%;
    /* Full width */
    height: 100%;
    /* Full height */
    overflow: auto;
    /* Enable scroll if needed */
    background-color: rgb(0, 0, 0);
    /* Fallback color */
    background-color: rgba($body-bg, 0.4);
    /* Black w/ opacity */
  }

  /* Modal Content */
  .blackJack .modal-content {
    position: relative;
    background: rgba($form-bg, .9);
    margin: auto;
    padding: 0;
    width: 80%;
    box-shadow: 0 0 100px 50px rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
    -webkit-animation-name: animatetop;
    -webkit-animation-duration: 0.4s;
    animation-name: animatetop;
    animation-duration: 0.4s
  }

  /* Add Animation */
  @-webkit-keyframes animatetop {
    from {
      top: -300px;
      opacity: 0
    }

    to {
      top: 0;
      opacity: 1
    }
  }

  @keyframes animatetop {
    from {
      top: -300px;
      opacity: 0
    }

    to {
      top: 0;
      opacity: 1
    }
  }

  /* The Close Button */
  .blackJack .close {
    color: white;
    float: right;
    font-size: 28px;
    font-weight: bold;
  }

  .blackJack .close:hover,
  .blackJack .close:focus {
    color: white;
    text-decoration: none;
    cursor: pointer;
  }

  .blackJack.req {
    margin: 2px;
    color: #1ab188;
    ;
  }

  .blackJack .modal-header {
    padding: 10px 16px;
    text-align: center;
    background: #222222a8;
    color: white;
  }

  .blackJack .modal-body {
    padding: 20px 16px;
    text-align: center;
  }

  .blackJack .modal-footer {
    padding: 15px 16px;
    background-color: #222222a8;
    color: white;
  }
</style>

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

  @keyframes Infos {
    0% {
      opacity: 1;
    }

    50% {
      opacity: 0.5;
    }

    100% {
      opacity: 1;
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

  .blackJack #Infos {
    animation: Infos 2s infinite;
  }

  #deck {
    height: 215px;
    width: 135px;
  }


  #deck1 {
    margin-left: 680px;
  }

  #wait {
    visibility: hidden;
  }

  .playercards>img {
    height: 205px;
    width: 125px;
  }

  .playercards {
    display: inline-block;
    position: relative;

  }

  .dealercards {
    display: inline;
    position: relative;
  }

  .dealercards>img {
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