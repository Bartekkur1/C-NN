using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTest
{
    class ActivationFunction
    {
        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public static double Dsigmoid(double x)
        {
            return x * (1 - x);
        }
    }

    class NeuralNetwork
    {
        int input_nodes, hidden_nodes, output_nodes;
        public double learning_rate = 0.01;
        Matrix weights_ih, weights_ho, bias_h, bias_o;

        public NeuralNetwork(int _input_nodes, int _hidden_nodes, int _output_nodes)
        {
            this.input_nodes = _input_nodes;
            this.hidden_nodes = _hidden_nodes;
            this.output_nodes = _output_nodes;
            this.weights_ih = new Matrix(hidden_nodes, input_nodes);
            this.weights_ho = new Matrix(output_nodes, hidden_nodes);
            this.weights_ih.RandomizeF();
            this.weights_ho.RandomizeF();
            this.bias_h = new Matrix(hidden_nodes, 1);
            this.bias_o = new Matrix(output_nodes, 1);
            this.bias_h.RandomizeF();
            this.bias_o.RandomizeF();
        }

        public void SetLearningRate(double x)
        {
            this.learning_rate = x;
        }

        public double[] FeedForward(double[] arr)
        {
            Matrix inputs = Matrix.FromArray(arr);
            Matrix hidden = Matrix.Multiply(weights_ih, inputs);
            hidden.Add(bias_h);
            hidden = Matrix.SigmoidElements(hidden);

            Matrix output = Matrix.Multiply(weights_ho, hidden);
            output.Add(bias_o);
            output = Matrix.SigmoidElements(output);

            return output.ToArray();
        }

        public void Train(double[] input_array, double[] target_array)
        {
            Matrix inputs = Matrix.FromArray(input_array);
            Matrix hidden = Matrix.Multiply(weights_ih, inputs);
            hidden.Add(bias_h);
            hidden = Matrix.SigmoidElements(hidden);

            Matrix outputs = Matrix.Multiply(weights_ho, hidden);
            outputs.Add(bias_o);
            outputs = Matrix.SigmoidElements(outputs);

            Matrix targets = Matrix.FromArray(target_array);

            Matrix output_errors = Matrix.Subtract(targets, outputs);

            Matrix gradients = Matrix.DsigmoidElements(outputs);
            gradients.Multiply(output_errors);
            gradients.Multiply(learning_rate);

            Matrix hidden_T = Matrix.Transpose(hidden);
            Matrix weight_ho_deltas = Matrix.Multiply(gradients, hidden_T);

            weights_ho.Add(weight_ho_deltas);
            bias_o.Add(gradients);

            Matrix who_t = Matrix.Transpose(weights_ho);
            Matrix hidden_errors = Matrix.Multiply(who_t, output_errors);

            Matrix hidden_gradient = Matrix.DsigmoidElements(hidden);
            hidden_gradient.Multiply(hidden_errors);
            hidden_gradient.Multiply(learning_rate);

            Matrix inputs_T = Matrix.Transpose(inputs);
            Matrix weights_ih_deltas = Matrix.Multiply(hidden_gradient, inputs_T);

            weights_ih.Add(weights_ih_deltas);
            bias_h.Add(hidden_gradient);

            //targets.Print();
        }

        public double[] Predict(double[] input_array)
        {
            Matrix inputs = Matrix.FromArray(input_array);
            Matrix hidden = Matrix.Multiply(weights_ih, inputs);
            hidden.Add(bias_h);
            hidden = Matrix.SigmoidElements(hidden);

            Matrix output = Matrix.Multiply(this.weights_ho, hidden);
            output.Add(bias_o);
            output = Matrix.SigmoidElements(output);

            return output.ToArray();
        }
    }
}
