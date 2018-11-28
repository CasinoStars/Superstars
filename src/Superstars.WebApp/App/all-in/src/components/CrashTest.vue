<template>
    <div>
        <center>
            <div class="piecontainer" id="pie-container">
                <canvas id="pie-chart" class="chartjs" width="770px" height="385px">
                </canvas>
                x{{multi}}
            </div>
        </center>
    </div>
</template>
<script src="~/lib/signalr/signalr.js"></script>

<script>
    import {
        mapActions
    } from 'vuex';
    import Vue from 'vue';
    import GameApiService from '../services/GameApiService';
    import CrashApiService from '../services/CrashApiService';
    import Chart from 'chart.js';

    export default {
        data() {
            return {
                chart: null,
                i: null,
                scale: null,
                multi: 1,
                fn: null,
                connection: null

            }
        },

        async mounted() {

            var signalR = require("@aspnet/signalr");
            this.connection = new signalR.HubConnectionBuilder().withUrl("/SignalR").configureLogging(signalR.LogLevel.Error).build();
            this.connection.on("Newgame", ite => {
                console.log(ite)
                this.chart.destroy();
                this.initializeChart();
                this.i = 0
                this.myLoop(ite);
            });
            this.connection.start();

            this.initializeChart();
            this.i = 0;
            var ite = await CrashApiService.GetNextCrash();

            
        },

        methods: {
            initializeChart() {
                this.chart = new Chart(document.getElementById("pie-chart"), {
                    // The type of chart we want to create
                    type: 'line',

                    // The data for our dataset
                    data: {
                        datasets: [{
                            borderColor: 'rgb(255, 99, 132)',
                            data: [],
                            showLine: false, // disable for a single dataset
                        }]
                    },

                    // Configuration options go here
                    options: {
                        showLine: false, // disable for a single dataset
                        elements: {
                            line: {
                                tension: 0, // disables bezier curves
                            }
                        },
                        scales: {
                            xAxes: [{
                                type: "linear",
                                ticks: {
                                    suggestedMin: 1,
                                    suggestedMax: 10
                                }
                            }],
                            yAxes: [{
                                type: "linear",
                                ticks: {
                                    suggestedMin: 1,
                                    suggestedMax: 2
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
                chart.data.datasets.forEach((dataset) => {
                    dataset.data.push({
                        x: x,
                        y: y
                    });
                });
                chart.update();
            },
            myLoop(iteration) {
                this.fn = setInterval(() => {
                    this.addData(this.chart, this.i / 10 + 1, Math.exp(this.i / 100));
                    this.multi = Math.exp(this.i / 100);
                    this.multi = Math.round(this.multi * 100) / 100;
                    this.i++;
                    if (this.multi >= iteration) {
                        this.multi = iteration;
                        clearInterval(this.fn);
                    }
                }, 100);
                
            }
        }
    }
</script>


<style lang="scss">
    // #pie-chart {
    //    width: 10px;
    //    height: 10px;
    // }
    .piecontainer {
        height: 800px;
        width: 800px;
        display: inline-block;
    }

    table {
        font-family: arial, sans-serif;
        border-collapse: collapse;
        border-spacing: 0;
        width: 100%;
        border: 1px solid #ddd;
    }

    tr {
        background-color: #f2f2f2;
    }

    td,
    th {
        border-bottom: 1px solid #dddddd;
        text-align: left;
        padding: 14px;
        font-family: 'Courier New', sans-serif;
        font-size: 21px;
        font-variant: small-caps;
        font-weight: bold;
    }

    th {
        background-color: #343a40;
        color: white;
    }
</style>