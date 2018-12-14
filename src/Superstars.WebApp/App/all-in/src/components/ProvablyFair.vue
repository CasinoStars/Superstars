<template>
  <div class="provablyfair">
    <!-- errors of transactions !-->
    <div class="alert alert-danger" style="text-align: center;" v-if="errors.length > 0">
      <li v-for="e of errors" :key="e">{{e}}</li>
    </div>

    <!-- Upgrade -->

    <div class="form" style="letter-spacing: 2px; font-family: 'Courier New', sans-serif;">

      <ul class="tab-group">
        <li style="color: white" class="tab active" v-if="this.view == 'provablyFair'"><a v-on:click="ChangeView('provablyFair')">Seeds</a></li>
        <li style="color: white" class="tab" v-else><a v-on:click="ChangeView('provablyFair')">Seeds</a></li>

                <li style="color: white" class="tab active" v-if="this.view == 'provablyFairTest'"><a v-on:click="ChangeView('provablyFairTest')">Retrouver les dés</a></li>
                <li style="color: white" class="tab" v-else><a v-on:click="ChangeView('provablyFairTest')">Retrouver les dés</a></li>
                
                
                <li style="color: white" class="tab active" v-if="this.view == 'provablyFairCode'"><a v-on:click="ChangeView('ProvablyFairCode')" style="margin-leftg: 25%;">Code</a></li>
                <!-- <li style="color: white" class="tab" v-else><a v-on:click="ChangeView('provablyFairCode')" style="margin-leftg: 25%;">Code</a></li> -->
            </ul>

            <div style="color: white" v-if="this.view == 'provablyFair'">
                <div class="tab-content">
                    <h4 style="color: white"><span style="font-weight: bold; font-style: italic;">Clef client:</span><br>{{seeds.clientSeed}}<br></h4>
                    <h4 style="color: white"><span style="font-weight: bold; font-style: italic;">Clef Serveur:</span><br>{{seeds.cryptedServerSeed}}<br></h4>
                    <h4 style="color: white"><span style="font-weight: bold; font-style: italic;">Nombre de dés générer par le hash:</span><br>{{seeds.nonce}}<br></h4>
                    
                    
                    <h4 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef serveur:</span><br>{{seeds.uncryptedPreviousServerSeed}}<br></h4>
                    <h4 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef client:</span><br>{{seeds.previousClientSeed}}<br></h4>
                    <h4 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef serveir crypté:</span><br>{{seeds.previousCryptedServerSeed}}<br></h4>

                    <form  @submit="UpdateSeeds($event)">
                            <div class="field-wrap">
                                <label style="margin-left: 35%; ">
                                    <span style="font-weight: bold; font-style: italic;">
                                    Clef client<span class="req">*</span> 
                                    </span>
                                </label><br><br>
                                <input  type="string" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="clientSeeds" required autocomplete="off"/>
                            </div>

      <div style="color: white" v-if="this.view == 'provablyFair'">
        <div class="tab-content">
          <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Clef client:</span><br><div style="font-size:70%;">{{seeds.clientSeed}} <a style="cursor: pointer;" @click="Copy(seeds.clientSeed)"><i class="fa fa-files-o"></i></a></div><br></h5>
          <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Clef Serveur:</span><br><div style="font-size:70%;">{{seeds.cryptedServerSeed}} <a style="cursor: pointer;" @click="Copy(seeds.cryptedServerSeed)"><i class="fa fa-files-o"></i></a></div><br></h5>
          <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Nombre de dés générer par le hash:</span><br>{{seeds.nonce}}<br><br></h5>
          <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef serveur:</span><br>{{seeds.uncryptedPreviousServerSeed}}<br><br></h5>
          <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef client:</span><br>{{seeds.previousClientSeed}}<br><br></h5>
          <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef serveur crypté:</span><br>{{seeds.previousCryptedServerSeed}}<br><br></h5>

          <form @submit="UpdateSeeds($event)">
            <div class="field-wrap">
              <label style="margin-left: 45%; ">
                <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">
                  Clef client<span class="req">*</span>
                </span></h5>
              </label><br><br>
              <input type="string" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="clientSeeds"
                required autocomplete="off" />
            </div>

            <button type="submit" class="button button-block">Changer</button>
          </form>

        </div><!-- tab-content -->
      </div>

      <div style="color: white" v-if="this.view == 'provablyFairTest'">
        <div class="tab-content">
          <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Test:</span><br><div style="font-size:70%;">{{seeds.clientSeed}} <a style="cursor: pointer;" @click="Copy(seeds.clientSeed)"><i class="fa fa-files-o"></i></a></div><br><br></h5>

          <form @submit="RetriveDicesFromSeeds($event)">
            <div class="field-wrap">
              <label style="margin-left: 45%; ">
                <span style="font-weight: bold; font-style: italic;">
                  Seed Client<span class="req">*</span>
                </span>
              </label><br><br>
              <input type="string" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="clientSeedTest"
                required autocomplete="off" />
            </div>
        </div>
      </div>
</template>

<script>
  import {
    mapActions
  } from "vuex";
  import ProvablyFairApiService from "../services/ProvablyFairApiService";

  export default {
    data() {
      return {
        serverSeedTest: "",
        clientSeedTest: "",
        nbOfDices: 0,
        clientSeeds: "",
        seeds: "",
        view: "provablyFair",
        dicesFromSeeds: [],
        errors: [],
        Responses: []
      };
    },

    mounted() {
      this.GetSeeds();
    },

    methods: {
      ...mapActions(["executeAsyncRequest"]),

      openjsfiddle() {
        window.open("https://jsfiddle.net/so1kv0uj/2/", "_blank");
      },

      Copy(copy) {
        navigator.clipboard.writeText(copy);
      },

      async GetSeeds() {
        this.seeds = await this.executeAsyncRequest(() =>
          ProvablyFairApiService.GetSeeds()
        );
      },

      async RetriveDicesFromSeeds(e) {
        e.preventDefault();
        let toto = this;
        let azerty = await ProvablyFairApiService.RetriveDicesFromSeeds(
          this.serverSeedTest,
          this.clientSeedTest,
          this.nbOfDices
        ).then(function (azerty) {
          console.log("The last one");
          console.log(azerty);
          toto.dicesFromSeeds = azerty;
        });
      },

      async UpdateSeeds(e) {
        e.preventDefault();
        var errors = [];

        await this.executeAsyncRequest(() =>
          ProvablyFairApiService.UpdateSeeds(this.clientSeeds)
        );
        this.GetSeeds();
      },

      ChangeView(view) {
        this.view = view;
        this.errors = 0;
      }
    }
  };
</script>

<style lang="scss">
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

  #snackbar {
    visibility: hidden;
    min-width: 250px;
    margin-left: -11%;
    background-color: #333;
    color: #fff;
    text-align: center;
    border-radius: 2px;
    padding: 16px;
    position: fixed;
    z-index: 1;
    left: 50%;
    bottom: 30px;
    font-size: 17px;
  }

  #snackbar.show {
    visibility: visible;
    -webkit-animation: fadein 0.5s, fadeout 0.5s 3s;
    animation: fadein 0.5s, fadeout 0.5s 3s;
  }

  @-webkit-keyframes fadein {
    from {
      bottom: 0;
      opacity: 0;
    }

    to {
      bottom: 30px;
      opacity: 1;
    }
  }

  @keyframes fadein {
    from {
      bottom: 0;
      opacity: 0;
    }

    to {
      bottom: 30px;
      opacity: 1;
    }
  }

  @-webkit-keyframes fadeout {
    from {
      bottom: 30px;
      opacity: 1;
    }

    to {
      bottom: 0;
      opacity: 0;
    }
  }

  @keyframes fadeout {
    from {
      bottom: 30px;
      opacity: 1;
    }

    to {
      bottom: 0;
      opacity: 0;
    }
  }

  .provablyfair html {
    overflow-y: scroll;
  }

  .provablyfair body {
    background: $body-bg;
    font-family: "Titillium Web", sans-serif;
  }

  .provablyfair .mytitle {
    text-decoration: none;
    color: $main;
    transition: 0.5s ease;

    &:hover {
      color: $main-dark;
    }
  }

  .provablyfair .form {
    background: rgba($form-bg, 0.9);
    padding: 40px;
    max-width: 6000px;
    margin: 40px auto;
    border-radius: $br;
    box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
  }

  .provablyfair .tab-group {
    list-style: none;
    padding: 0;
    margin: 0 0 40px 0;

.provablyfair body {
  font-family: "Titillium Web", sans-serif;
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

.provablyfair .form {
  background: rgba($form-bg, 0.9);
  padding: 40px;
    height: 800px;
  max-width: 6000px;
  border-radius: $br;
  box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
}

    .active a {
      background: $main;
      color: $white;
    }
  }

  .provablyfair .tab-content>div:last-child {
    display: none;
  }

  .provablyfair h5 {
    text-align: center;
    color: $white;
    font-weight: $thin;
  }

  .provablyfair label {
    position: absolute;
    transform: translateY(6px);
    left: 13px;
    color: $white;
    transition: all 0.25s ease;
    -webkit-backface-visibility: hidden;
    pointer-events: none;
    font-size: 22px;

.provablyfair h5, h4 {
  text-align: center;
  color: $white;
  font-weight: $thin;
}

  .provablyfair input,
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

.provablyfair input,
textarea {
  font-size: 22px;
  //display: block;
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

  .provablyfair .field-wrap {
    position: relative;
    margin-bottom: 10px;
  }

.provablyfair .field-wrap {
  position: relative;
  margin-bottom: 10px;
  
}

    &:hover,
    &:focus {
      background: $main-dark;
    }
  }

  .provablyfair .button-block {
    display: block;
    width: 100%;
  }

  .provablyfair .forgot {
    margin-top: -20px;
    text-align: right;
  }
  }
  }
</style>