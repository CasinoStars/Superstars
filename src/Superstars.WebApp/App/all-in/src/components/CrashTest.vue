<template>
    <div class=CrashTest>

        <!-- The Modal -->
        <div id="myModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content">

                <div class="modal-header">
                    <div style="padding-top: 2px; font-family: 'Courier New', sans-serif;" class="col-12 modal-title text-center">
                        <router-link class="close" v-on:click.native="closeModal()" to="">&times;</router-link>
                    </div>
                </div>


                <div class="modal-body">
                    Numéro de la partie: {{meta.gameId}}
                    <br>
                    Date de début: {{meta.startDate}}
                    <br>
                    Date de fin: {{meta.endDate}}
                    <br>
                    Hash: {{meta.crashHash}}
                    <br>
                    X: {{meta.crashValue}}
                    <div class="component-box-player-list col">
                        <table class="playerlist-table table table-striped table-bordered table-condensed table-hover">
                            <thead class="table-header">
                                <tr>
                                    <th>Joueur</th>
                                    <th class="text-right">X</th>
                                    <th class="text-right">Mise</th>
                                    <th class="text-right">Profit</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(e, index) of playersGame" :key="index">
                                    <td>{{e.userName}}</td>
                                    <td class="text-right">{{e.multi}}</td>
                                    <td class="text-right">{{e.bet.toLocaleString('en')}}</td>
                                    <td class="text-right" style="color: green" v-if="meta.crashValue > e.multi">{{profit(meta.crashValue,
                                        e.bet, e.multi).toLocaleString('en')}}</td>
                                    <td class="text-right" style="color: red" v-else>{{profit(meta.crashValue, e.bet,
                                        e.multi).toLocaleString('en')}}</td>

                                </tr>
                            </tbody>
                        </table>
                        <div>
                            <div class="row">
                                <div class="table-responsive col">
                                    <table class="playerlist-stats-table table table-striped table-condensed">
                                        <tbody>
                                            <tr class="table-footer">
                                                <td>
                                                    <center>Joueurs: {{playersGame.length}}</center>
                                                </td>
                                                <td>
                                                    <center>Mises: {{totalGameBet}} bits</center>
                                                </td>
                                                <td>
                                                    <center>Profits: {{totalGameProfit.toLocaleString('en')}} bits</center>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                </div>

            </div>
        </div>

        <div class="row">
            <div class="col-md-7">
                <div class="row">
                    <div class="col-md-8">
                        <div class="piecontainer" id="pie-container">
                            <canvas id="linechart">
                                <center v-if="!isWaiting">
                                    x{{multi}}
                                </center>
                                <center v-else>
                                    En attente des mises
                                </center>
                            </canvas>
                        </div>
                    </div>
                    <div class="col-md-4" style="margin-top:3%;">
                        <form @submit="toBet($event)">
                            <h4 style="text-align: left; color: black; font-family: 'Courier New', sans-serif;">MISE<span class="req">*</span></h4>
                            <input type="number" min=1 required v-model="bet" style="width:147px;"/>
                            <button type="button" @click="moneyType = true" id="BitsMoney" class="btn btn-dark" style="background-color: #e58929; font-size:70%; margin-left: -1px; margin-top:-1.3%;">BITS</button>
                            <button type="button" @click="moneyType = false" id="AllinMoney" class="btn btn-dark" style="font-size:70%; margin-left: -3px; margin-top:-1.3%;">ALL`IN</button>
                            <h4 style="text-align: left; color: black; font-family: 'Courier New', sans-serif; margin-top:5%;">MULTIPLICATEUR<span
                                    class="req">*</span></h4>
                            <input type="number" min=1 step=0.01 required
                                v-model="playerMulti" />
                            <div style="margin-right: 42%; margin-top:3%;">
                                <button type="submit" class="btn btn-primary" v-if="isWaiting && !hasPlayed">Confirmer</button>
                                <button type="button" @click="out()" class="btn btn-success" v-else-if="!isWaiting && hasPlayed && !isOut">Sortir</button>
                                <button disabled type="submit" class="btn btn-primary" v-else>Confirmer</button>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="row">
                    <table class="playerlist-table table table-striped table-bordered table-condensed table-hover col"
                        style="margin-left:10px">
                        <thead class="table-header">
                            <tr>
                                <th>Hash</th>
                                <th class="text-right">X</th>
                                <th>Mise</th>
                                <th class="text-right">Multi</th>
                                <th>Profit</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(e, index) of hashList" :key="index" >
                                <td><input class="hash" readonly v-model="e.crashHash"></td>
                                <td v-on:click="showPlay(e.gameId)" class="text-right" style="color: green" v-if="e.crashValue >= 2">{{e.crashValue}}</td>
                                <td v-on:click="showPlay(e.gameId)" class="text-right" style="color: red" v-else>{{e.crashValue}}</td>
                                <td v-on:click="showPlay(e.gameId)">{{e.bet}}</td>
                                <td v-on:click="showPlay(e.gameId)" class="text-right">{{e.multi}}</td>
                                <td v-on:click="showPlay(e.gameId)">{{profit(e.crashValue, e.bet, e.multi).toLocaleString('en')}}</td>


                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="component-box-player-list col">
                <table class="playerlist-table table table-striped table-bordered table-condensed table-hover">
                    <thead class="table-header">
                        <tr>
                            <th>Joueur</th>
                            <th class="text-right">X</th>
                            <th class="text-right">Mise</th>
                            <th class="text-right">Profit</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(e, index) of playersData" :key="index">
                            <td>{{e.userName}}</td>
                            <td class="text-right">{{e.multi}}</td>
                            <td class="text-right">{{e.bet.toLocaleString('en')}}</td>
                            <td class="text-right" v-if="multi < e.multi && multi != 0">{{(multi * e.bet
                                -e.bet).toLocaleString('en')}}</td>
                            <td class="text-right" v-else-if="multi >= e.multi && multi != 0">{{profit(multi, e.bet,
                                e.multi).toLocaleString('en')}}</td>
                            <td class="text-right" v-else>{{0}}</td>


                        </tr>
                    </tbody>
                </table>
                <div>
                    <div class="row">
                        <div class="table-responsive col">
                            <table class="playerlist-stats-table table table-striped table-condensed">
                                <tbody>
                                    <tr class="table-footer">
                                        <td>
                                            <center>Joueurs: {{playersData.length}}</center>
                                        </td>
                                        <td>
                                            <center>Mises: {{totalBet}} bits</center>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script src="~/lib/signalr/signalr.js"></script>


<script>
    import {
        mapActions,
        mapGetters
    } from 'vuex';
    import Vue from 'vue';
    import GameApiService from '../services/GameApiService';
    import CrashApiService from '../services/CrashApiService';
    import ChartJS from 'Chart.js';

    export default {
        data() {
            return {
                chart: null,
                i: null,
                scale: null,
                multi: 1,
                fn: null,
                connection: null,
                bet: null,
                playerMulti: null,
                moneyType: true,
                playersData: [],
                totalBet: 0,
                isWaiting: false,
                hasPlayed: false,
                points: [],
                ctx: null,
                hashList: null,
                meta: {},
                playersGame: [],
                totalGameBet: 0,
                totalProfit: 0,
                totalGameProfit: 0,
                isOut: false,
                definitiveMulti: 0,
                lineColor: "black"
            }
        },

        computed: {
            ...mapGetters(['BTCMoney']),
            ...mapGetters(['fakeMoney'])
        },
        
        watch: {
            moneyType: function () {
                if(this.moneyType) {
                    document.getElementById("BitsMoney").style.backgroundColor = "#e58929";
                    document.getElementById("AllinMoney").style.backgroundColor = "#343a40";
                } else {
                    document.getElementById("BitsMoney").style.backgroundColor = "#343a40";
                    document.getElementById("AllinMoney").style.backgroundColor = "#4a50b4";
                }
            }
        },

        async mounted() {

            this.hashList = await CrashApiService.GetHashList();
            console.log(this.hashList);
            var signalR = require("@aspnet/signalr");
            this.connection = new signalR.HubConnectionBuilder().withUrl("/SignalR").configureLogging(signalR.LogLevel
                .Error).build();
            this.connection.on("NewGame", async () => {
                console.log("start")
                this.isWaiting = false;
                this.isOut = false;
                //this.myLoop();
                if (this.hasPlayed)
                    this.lineColor = "green";
                await this.RefreshBTC();
                await this.RefreshFakeCoins();
            });
            this.connection.on("EndGame", async (gameId, hash, ite) => {

                console.log("Chart stop at " + this.multi + " serv stop at " + ite)

                this.lineColor = "red"
                this.chart.data.datasets.forEach((dataset) => {
                    dataset.borderColor = this.lineColor
                });
                this.chart.update()
                this.updateNumber()

                this.multi = ite;

                clearInterval(this.fn);
                if (!this.hasPlayed) {
                    this.hashList.unshift({
                        gameId: gameId,
                        crashHash: hash,
                        crashValue: ite,
                        bet: 0,
                        multi: 0
                    });
                } else {
                    this.hashList.unshift({
                        gameId: gameId,
                        crashHash: hash,
                        crashValue: ite,
                        bet: this.bet,
                        multi: this.definitiveMulti
                    });
                }
                if (this.hashList.length >= 10)
                    this.hashList.pop();


                await this.RefreshBTC();
                await this.RefreshFakeCoins();
            })
            this.connection.on("Step", async (ite, i) => {
                console.log("step at " + ite)
                this.multi = ite;
                this.addData(this.chart, i / 10 + 1, ite);
                this.updateNumber();
            })
            this.connection.on("Wait", async () => {
                console.log("wait")
                this.multi = 0;
                this.definitiveMulti = 0;
                this.isWaiting = true
                this.hasPlayed = false;
                this.totalBet = 0;
                this.points = [];
                this.chart.destroy();
                this.initializeChart();
                var canvas = document.getElementById("linechart");
                var ctx = canvas.getContext("2d");
                ctx.font = "5vh Arial";
                ctx.strokeStyle = "black";
                ctx.textAlign = "center";
                ctx.strokeText("En attente des mises", canvas.scrollWidth/2, canvas.scrollHeight/2);
                this.lineColor = "black";
                this.i = 0
                this.playersData = [];
                await this.RefreshBTC();
                await this.RefreshFakeCoins();
            });

            this.connection.on("Bet", async (data) => {
                this.getPlayers(data);
            });

            this.connection.on("Update", async (data) => {
                this.playersData.forEach(element => {
                    if (element.userName == data.userName) {
                        element.multi = data.multi;
                    }
                });
            });
            this.connection.start();
            this.initializeChart();
            this.i = 0;
        },
        beforeDestroy() {
            this.chart.destroy();
            this.connection.stop();
            this.i = 0
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),
            ...mapActions(['RefreshFakeCoins']),
            ...mapActions(['RefreshBTC']),

            profit(Crash, bet, multi) {
                if (Crash < multi) {
                    return 0 - bet;
                } else {
                    return bet * multi - bet;
                }
            },
            async showPlay(gameId) {
                var modal = document.getElementById('myModal');
                modal.style.display = "block";
                this.meta = await CrashApiService.GetCrashMeta(gameId);
                this.playersGame = await CrashApiService.GetPlayersInGame(gameId);
                this.playersGame.forEach(e => {
                    this.totalGameBet += e.bet;
                    this.totalGameProfit += this.profit(this.meta.crashValue, e.bet, e.multi)
                });

            },
            closeModal() {
                var modal = document.getElementById('myModal');
                modal.style.display = "none";
                this.totalGameBet = 0;
                this.totalGameProfit = 0;
            },
            updateNumber() {
                var canvas = document.getElementById("linechart");
                var ctx = canvas.getContext("2d");
                ctx.font = "7vh Arial";
                ctx.textAlign ="center"
                ctx.strokeText("X" + this.multi.toFixed(2), canvas.scrollWidth/2, canvas.scrollHeight/2);
            },


            initializeChart() {
                this.chart = new Chart(document.getElementById("linechart"), {
                    // The type of chart we want to create
                    type: 'line',

                    // The data for our dataset
                    data: {

                        datasets: [{

                            label: "",
                            fill: false,
                            borderColor: this.lineColor,
                            data: [],
                            pointRadius: 0,
                        }]
                    },

                    // Configuration options go here
                    options: {
                        legend: {
                            display: false
                        },
                        tooltips: {
                            enabled: false
                        },
                        scales: {

                            xAxes: [{
                                gridLines: {
                                    display: false
                                },
                                type: "linear",
                                ticks: {
                                    suggestedMin: 1,
                                    suggestedMax: 3,
                                    precision: 2
                                }
                            }],
                            yAxes: [{
                                gridLines: {
                                    display: false
                                },
                                type: "linear",
                                ticks: {
                                    suggestedMin: 1,
                                    suggestedMax: 3,
                                    precision: 2
                                }
                            }]
                        },
                        animation: {
                            duration: 0, // general animation time
                        },
                        hover: {
                            animationDuration: 0, // duration of animations when hovering an item
                        },

                        responsiveAnimationDuration: 0 // animation duration after a resize
                    }
                });
            },

            addData(chart, x, y) {
                var maxY = y + 2
                var maxX = x + 2

                chart.data.datasets.forEach((dataset) => {
                    dataset.data.push({
                        x: x,
                        y: y
                    });
                    dataset.borderColor = this.lineColor
                });
                chart.options.scales = {
                    xAxes: [{
                        gridLines: {
                            display: false
                        },
                        type: "linear",
                        ticks: {
                            min: 1,
                            max: maxX,
                            precision: 2

                        }
                    }],

                    yAxes: [{
                        gridLines: {
                            display: false
                        },
                        drawOnChartArea: false,
                        ticks: {
                            min: 1,
                            max: maxY,
                            precision: 2
                        }
                    }],

                }
                if (this.multi > +this.definitiveMulti && this.hasPlayed) {
                    this.lineColor = "black";
                    this.isOut = true;
                }

                chart.update();
            },

            myLoop() {
                this.fn = setInterval(() => {
                    this.addData(this.chart, this.i / 10 + 1, Math.exp(this.i / 100));
                    this.multi = Math.exp(this.i / 100);
                    this.multi = Math.round(this.multi * 100) / 100;
                    this.i++;
                }, 100);
            },
            async toBet(e) {
                e.preventDefault();
                this.hasPlayed = true;
                var errors = [];
                this.errors = 0;
                if (this.moneyType === false) {
                    if (this.bet > 1000000)
                        errors.push("La mise maximum est de 1,000,000");
                    else if (this.bet <= 0)
                        errors.push("La mise doit être supérieur à 0");
                    else if (this.bet > this.fakeMoney)
                        errors.push("Vous n'avez pas cette somme sur votre compte");
                } else {
                    if (this.bet > 10000000)
                        errors.push("La mise maximum est de 10,000,000 bits");
                    else if (this.bet <= 0)
                        errors.push("La mise doit être supérieur à 0 bits");
                    else if (this.bet > this.BTCMoney)
                        errors.push("Vous n'avez pas cette somme sur votre compte");
                }
                this.errors = errors;
                if (errors.length == 0) {
                    try {
                        if (this.moneyType === false) {
                            await this.executeAsyncRequest(() => GameApiService.BetCrash(this.bet, this.playerMulti,
                                0));

                            //await this.RefreshFakeCoins();

                            this.success = 'Vous venez de parier: ' + this.bet + ' All`In Coins';
                            this.definitiveMulti = this.playerMulti;


                        } else {
                            await this.executeAsyncRequest(() => GameApiService.BetCrash(this.bet, this.playerMulti,
                                1));

                            await this.RefreshBTC();
                            this.success = 'Vous venez de parier: ' + this.bet + ' Bits';
                            this.definitiveMulti = this.playerMulti;
                        }

                    } catch (error) {
                        console.log(error)
                    }

                }
            },

            getPlayers(data) {
                this.playersData.push(data)
                this.playersData.forEach(elements => {
                    this.totalBet += elements.bet;
                });
            },
            async out() {
                this.isOut = true;
                this.definitiveMulti = this.multi;
                await this.executeAsyncRequest(() => GameApiService.UpdateCrash(this.multi));
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

    .hash {
        //border: 1px solid inherit;
        border: none;
        width: 100%;
        background-color: inherit;
        border-radius: 1px;
        font-family: 'Courier New', sans-serif;
        font-size: 21px;
        font-variant: small-caps;
        font-weight: bold;
    };

    .CrashTest .close {
        color: white;
    }
    
    .CrashTest .modal {
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
    .CrashTest .modal-content {
        position: relative;
        background: rgba($form-bg, .9);
        margin: auto;
        padding: 0;
        width: 80%;
        color: white;
        box-shadow: 0 0 100px 50px rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        -webkit-animation-name: animatetop;
        -webkit-animation-duration: 0.4s;
        animation-name: animatetop;
        animation-duration: 0.4s
    }

    
    .CrashTest .table-body .table-bordered th,.CrashTest .table-bordered td, .CrashTest .table-bordered tr{
        border : solid 1px  black !important
    }

    .CrashTest .modal-header {
        padding: 10px 16px;
        text-align: center;
        background: #222222a8;
        color: white;
    }

    .CrashTest .modal-body {
        padding: 20px 16px;
    }

    .CrashTest .modal-footer {
        padding: 15px 16px;
        background-color: #222222a8;
        color: white;
    }

    .piecontainer {
        height: 100%;
    }

    .text-right {
        text-align: right;
    }

    .CrashTest tr {
        background-color: #f2f2f2;
    }

    .CrashTest td,
    th {
        border-bottom: 1px solid #dddddd;
        text-align: left;
        padding: 14px;
        font-family: 'Courier New', sans-serif;
        font-size: 21px;
        font-variant: small-caps;
        font-weight: bold;
    }
    .CrashTest th {
        background-color: #343a40;
        color: white;
    }
    .onoffswitch {
        position: relative;
        width: 90px;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
    }

    .onoffswitch-checkbox {
        display: none;
    }

    .onoffswitch-label {
        display: block;
        overflow: hidden;
        cursor: pointer;
        border: 2px solid #999999;
        border-radius: 20px;
    }

    .onoffswitch-inner {
        display: block;
        width: 200%;
        margin-left: -100%;
        transition: margin 0.3s ease-in 0s;
    }

    .onoffswitch-inner:before,
    .onoffswitch-inner:after {
        display: block;
        float: left;
        width: 50%;
        height: 30px;
        padding: 0;
        line-height: 30px;
        font-size: 14px;
        color: white;
        font-family: Trebuchet, Arial, sans-serif;
        font-weight: bold;
        box-sizing: border-box;
    }

    .onoffswitch-inner:before {
        content: "Bitcoin";
        padding-left: 10px;
        background-color: #FF9900;
        color: #FFFFFF;
    }

    .onoffswitch-inner:after {
        content: "All'in";
        padding-right: 10px;
        background-color: #373C42;
        color: #CDCED0;
        text-align: right;
    }

    .onoffswitch-switch {
        display: block;
        width: 18px;
        margin: 6px;
        background: #FFFFFF;
        position: absolute;
        top: 0;
        bottom: 0;
        right: 56px;
        border: 2px solid #999999;
        border-radius: 20px;
        transition: all 0.3s ease-in 0s;
    }

    .onoffswitch-checkbox:checked+.onoffswitch-label .onoffswitch-inner {
        margin-left: 0;
    }

    .onoffswitch-checkbox:checked+.onoffswitch-label .onoffswitch-switch {
        right: 0px;
    }
</style>