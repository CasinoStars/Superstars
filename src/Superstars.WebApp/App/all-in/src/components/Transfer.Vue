
<template>
  <div class="Transfer">

    <!-- errors of transactions !-->
    <!-- Upgrade -->

      <!-- RealWallet -->
      
      <div class="form" style="letter-spacing: 2px; font-family: 'Courier New', sans-serif;">
                <h1>Transferts</h1>
                <div class="line"> </div>

      <form style="margin-top:6%;" @submit.prevent="Transfer($event, item.DestinationAccount)">

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
        <button type="submit" style="margin-bottom:5%;" class="button button-block">Envoyer</button>
        <div class="alert alert-danger" style="text-align: center; margin-top: 5%" v-if="errors.length > 0">
      <li v-for="e of errors" :key="e">{{e}}</li>
    </div>
           <div class="alert alert-success" style="text-align: center; margin-top: 5%" v-if="confirmation.length > 0">
      <li v-for="e of confirmation" :key="e">{{e}}</li>
    </div>
    <div class="transaction">         
          <table id="pendingTrx">
           <tr>
              <th>Historique des transferts</th>
            </tr>
            <div class = "pendingTrxList">
              <tr>
                  <div class = "formLine" v-for="transfer in TransferList" >
                   <td>You send {{transfer.amount}} 
                   to {{transfer.receiverName}} 
                   at {{transfer.transferDate}} </td>
            </div>                  
              </tr>
            </div>
            </table>
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
  import UserApiService from "../services/UserApiService";

  export default {
    data() {
      return {
        item: {},
        pseudo:'',
        trueBalance:'',
        success :'',
        errors: [],
        confirmation: [],
        TransferList: [],
        isPseudoExist: false,
        Responses: []
      };
    },

    computed: {
      ...mapGetters(["BTCMoney"]),
      ...mapGetters(["fakeMoney"])
    },

    async mounted() {
         await this.GetTransfer();
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
        var errors = [];
        var confirmation = [];  
        if(this.item.AmountToSend > this.trueBalance) {

        this.BTCAddress = await this.executeAsyncRequest(() =>
          WalletApiService.GetWalletAddress()
        );       
        }
        else {
            errors.push(
            "Vous n'avez pas assez de bits"
          );
        }

      },

      async GetTransfer() {
          this.TransferList = await this.executeAsyncRequest(() =>
          WalletApiService.GetTransfer()
        );
      },

      async GetPseudo() {
        this.pseudo = await this.executeAsyncRequest(() =>
        UserApiService.GetPseudo()
        );
      },

       async Transfer(e) { 
        
         this.isPseudoExist = await this.executeAsyncRequest(() =>
         WalletApiService.isPseudoExist(this.item)
         );
         var errors = [];
         var confirmation = [];

       //   e.preventDefault();
        if(this.isPseudoExist == "true") {
          console.log("GAGNERRR WE DID IT");
        

          this.trueBalance = await this.executeAsyncRequest(() =>
          WalletApiService.GetTrueBalance(this.item)
        );  

          if(this.item.AmountToSend < this.trueBalance) {
              await this.executeAsyncRequest(() =>
              WalletApiService.Transfer(this.item));
              confirmation.push("Vous avez envoyer " + this.item.AmountToSend + " Bits to " + this.item.DestinationAccount);
              this.confirmation = confirmation;
          }
          else{
                      errors.push("Vous avez pas assez de bits");
          }
        }
        else {
          errors.push("Le pseudo n'exist pas");
        }
                  this.errors = errors;
                  this.confirmation = confirmation;
       }
    }
  };
</script>

<style lang="scss">
  // @import "compass/css3";

  $body-bg: white;
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

.Transfer .button-block:hover {
  opacity: 1;
}
  .Transfer {
    margin-top: 7.5%;
  }

  .Transfer body {
    background: $body-bg;
    font-family: "Titillium Web", sans-serif;
  }

  .Transfer .formLine {
      border: 1px solid rgb(0, 0, 0);
  }

  //  .Transfer.td {
  //    width: 1000px;
  //  }

  .Transfer a {
    text-decoration: none;
    color:white;
    transition: 0.5s ease;

    &:hover {
      color: $main-dark;
    }
  }

  .Transfer .form {
    background: rgba($form-bg, 0.9);
    padding: 40px;
    height: 900px;
    width: 136%;
    border-radius: $br;
    box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
  }

.Transfer .tab-group {
  padding:0;
  margin:0 0 5px 0;
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
      color:white;
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

  .Transfer .pendingTrxList .formLine {
  padding: 1px;
  max-height: 125px;
  font-size: 30px;
  width: 111.7%;
  background-color: white;
}

.Transfer .pendingTrxList{
  overflow-y: scroll;
}

  .Transfer h5 {
    text-align: center;
    color: $white;
    font-weight: $thin;
  }

    .Transfer h1, .provablyfair h1, .settings h1 {
    padding: 15px;
    font-size: 45px;
    text-align: center;
    color:white;
  }

  .Transfer label {
    position: absolute;
    transform: translateY(6px);
    left: 13px;
    color:white;
    transition: all 0.25s ease;
    -webkit-backface-visibility: hidden;
    pointer-events: none;
    font-size: 22px;

    .req {
      margin: 2px;
      color: #1ab188;
    }
  }

   .line {
    margin-top: 6%;
    width: 100%;
    //height: 0.025%;
    height: 1px;
    background: white;

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
    color: white;
    border-radius: 0;
    transition:  box-shadow 0.25s ease;

    &:focus {
      outline: 0;
      border-color: $main;
    }
  }

  .Transfer textarea {
    border: 2px solid $gray-light;
    resize: vertical;
    
  }

  .Transfer h4 {
    color:white;
  }

  .Transfer .field-wrap {
    position: relative;
    margin-bottom: 10px;
    color:white;
  }

  .Transfer .button-block {
    display: block;
    width: 100%;
  }

  .forgot {
    margin-top: -20px;
    text-align: right;
  }
</style>