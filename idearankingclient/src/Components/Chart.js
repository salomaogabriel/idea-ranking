import React from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Line } from 'react-chartjs-2';
ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
  );
function Chart({labels, data, maxRating}) {
    const options = {
        responsive: true,
        scales: {
            y: {
              max: maxRating + 50,
            },
        },
        plugins: {
          legend: {
            position: 'top',
          },
          title: {
            display: true,
            text: 'Rating over time',
          },
        },
      };
      const dataFormatted = {
        labels,
        datasets: [
          {
            label: 'Rating',
            data: data,
            borderWidth: 1,
            pointRadius: 0,
            borderColor: '#FBA94C',
            backgroundColor: '#FBA94C2f',
          }
        ],
      };
        return (<Line options={options} data={dataFormatted} />
           );
}

export default Chart;

