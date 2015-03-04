namespace PrintServer
{
    using DYMO.Label.Framework;
    using System;

    class LabelPrintJob
    {
        private static const String PRINTER = "DYMO LabelWriter 450 DUO Tape";
        private String text;
        private String tape;

        public LabelPrintJob(String text, String tape = "Tape12mm")
        {
            this.text = text;
            this.tape = tape;
        }

        public void print()
        {
            ContinuousLabel label = new ContinuousLabel(tape, PaperOrientation.Landscape, ContinuousLabelLengthMode.Auto, 0);
            label.RootCell.Subcells.Add(new ContinuousLabelCell());
            TextObject textObject = new TextObject("txt0");
            label.RootCell.Subcells[0].LabelObject = textObject;
            label.SetObjectText("txt0", text);

            IPrinters availablePrinters = Framework.GetTapePrinters();
            label.Print(PRINTER);
        }
    }
}
