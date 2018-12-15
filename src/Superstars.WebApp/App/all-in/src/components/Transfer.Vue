
<template>
  <div class="Transfer">

    <!-- errors of transactions !-->
    <!-- Upgrade -->

      <!-- RealWallet -->
      <div class="form" style="letter-spacing: 2px; font-family: 'Courier New', sans-serif;">
      <form @submit="Transfer($event)">
        <div class="field-wrap">
          <label>
            Nom de compte<span class="req">*</span>
          </label><br><br>
          <input type="text" min="0" placeholder="Nom de compte" v-model="item.DestinationAccount" required
            autocomplete="off" />
        </div>
        <div class="field-wrap">
          <label>
            Montant<span class="req">*</span>
          </label><br><br>
          <input type="number" placeholder="Montant" v-model="item.AmountToSend" required autocomplete="off" />
        </div><br>
        <button type="submit" class="button button-block">Envoyer</button>
        <div class="alert alert-danger" style="text-align: center; margin-top: 5%" v-if="errors.length > 0">
      <li v-for="e of errors" :key="e">{{e}}</li>
    </div>    
      </form>      
  </div>
  </div>
</template>

<style lang="css">
</style>

<script>
  import {
    mapActions,
    mapGetters
  } from "vuex";
  import WalletApiService from "../services/WalletApiService";

  export default {
    data() {
      return {
        item: {},
        trueBalance:'',
        success :'',
        errors: [],
        Responses: []
      };
    },

    computed: {
      ...mapGetters(["BTCMoney"]),
      ...mapGetters(["fakeMoney"])
    },

    async mounted() {
      // await this.RefreshBTC();
      // await this.RefreshFakeCoins();
    },

    methods: {
      ...mapActions(["executeAsyncRequest"]),
      ...mapActions(["RefreshFakeCoins"]),
      ...mapActions(["RefreshBTC"]),

      async GetWalletAddress() {
         this.trueBalance = await this.executeAsyncRequest(() =>
          WalletApiService.GetTrueBalance()
        );  
        if(this.item.AmountToSend > this.trueBalance) {

        this.BTCAddress = await this.executeAsyncRequest(() =>
          WalletApiService.GetWalletAddress()
        );       
        }
        else {
            var errors = []
            errors.push(
            "Vous n'avez pas assez de bits"
          );
        }

      },

       async Transfer(e) { 
        //   this.trueBalance = await this.executeAsyncRequest(() =>
        //   WalletApiService.GetTrueBalance()
        // );  

        // console.log(this.trueBalance + "truebalance");
        // console.log(this.AmountToSend);
        //   if(this.item.AmountToSend > this.trueBalance) {
          await this.executeAsyncRequest(() =>
          WalletApiService.Transfer(this.item));
      //  }
      //   else {
      //    var errors = []
      //       errors.push(
      //       "Vous n'avez pas assez de bits");
      //  }
       }

      

      // async onSubmit(e) {
      //   e.preventDefault();

      //   var errors = [];

      //   this.errors = errors;

      //   if (errors.length == 0) {
      //     try {
      //       await this.executeAsyncRequest(() =>
      //         WalletApiService.AddCoins(this.item)
      //       );
      //       await this.RefreshFakeCoins();
      //       var x = document.getElementById("snackbar");
      //       x.className = "show";
      //       setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
      //     } catch (error) {}
      //   }
      // },     
    }
  };
</script>

<style lang="scss">
  // @import "compass/css3";

  $body-bg: #c1bdba;
  $form-bg: #13232f;
  $white: #ffffff;

  $main: #777c7b;
  $main-light: lighten($main, 5%);
  $main-dark: darken($main, 5%);

  $gray-light: #a0b3b0;
  $gray: #ddd;

  $thin: 300;
  $normal: 400;
  $bold: 600;
  $br: 4px;

  .Transfer html {
    overflow-y: scroll;
  }

  .Transfer {
    margin-top: 7.5%;
  }

  .Transfer body {
    background: $body-bg;
    font-family: "Titillium Web", sans-serif;
  }

  .Transfer a {
    text-decoration: none;
    color: $main;
    transition: 0.5s ease;

    &:hover {
      color: $main-dark;
    }
  }

  .Transfer .form {
    background: rgba($form-bg, 0.9);
    padding: 40px;
    height: 900px;
    width: 1000px;
    border-radius: $br;
    box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
  }

  .Transfer .tab-group {
    list-style: none;
    padding: 0;
    margin: 0 0 40px 0;

    &:after {
      content: "";
      display: table;
      clear: both;
    }

    li a {
      display: block;
      text-decoration: none;
      padding: 15px;
      background: rgba($gray-light, 0.25);
      color: $gray-light;
      font-size: 20px;
      float: left;
      width: 50%;
      text-align: center;
      cursor: pointer;
      transition: 0.5s ease;

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

  .tab-content>div:last-child {
    display: none;
  }

  .Transfer h5 {
    text-align: center;
    color: $white;
    font-weight: $thin;
  }

  .Transfer label {
    position: absolute;
    transform: translateY(6px);
    left: 13px;
    color: rgba($white, 0.5);
    transition: all 0.25s ease;
    -webkit-backface-visibility: hidden;
    pointer-events: none;
    font-size: 22px;

    .req {
      margin: 2px;
      color: #1ab188;
    }
  }

  .Transfer input,
  textarea {
    font-size: 22px;
    display: block;
    width: 100%;
    height: 100%;
    padding: 5px 10px;
    background: none;
    background-image: none;
    border: 1px solid $gray-light;
    color: $white;
    border-radius: 0;
    transition: border-color 0.25s ease, box-shadow 0.25s ease;

    &:focus {
      outline: 0;
      border-color: $main;
    }
  }

  .Transfer textarea {
    border: 2px solid $gray-light;
    resize: vertical;
  }

  .field-wrap {
    position: relative;
    margin-bottom: 10px;
  }

  .wallet .button {
    border: 0;
    outline: none;
    border-radius: 0;
    padding: 15px 0;
    font-size: 2rem;
    font-weight: $bold;
    text-transform: uppercase;
    letter-spacing: 0.1em;
    background: $main;
    color: $white;
    transition: all.5s ease;
    -webkit-appearance: none;

    &:hover,
    &:focus {
      background: $main-dark;
    }
  }

  .button-block {
    display: block;
    width: 100%;
  }

  .forgot {
    margin-top: -20px;
    text-align: right;
  }
</style>