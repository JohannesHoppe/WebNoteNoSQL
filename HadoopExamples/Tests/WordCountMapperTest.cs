using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using Machine.Specifications.Model;
using Moq;
using WordCountMapper;

using It = Machine.Specifications.It;

namespace Tests
{
    public class When_sending_data
    {
        private static Mock<IConsole> consoleMock;

        private Establish context = () =>
                                        {
                                            consoleMock = new Mock<IConsole>();
                                            consoleMock.SetupSequence(console => console.ReadLine())
                                                       .Returns("dies ist ein")
                                                       .Returns("test 22! test");
                                            Program.Console = consoleMock.Object;
                                        };

        private Because of = () => Program.Main(null);

        private It calls_console_readline = () => consoleMock.Verify(x => x.ReadLine(), Times.AtLeastOnce());

        private It maps_all_words_correctly1 = () => consoleMock.Verify(x => x.WriteLine(Moq.It.IsAny<string>(), "dies"), Times.Once());
        private It maps_all_words_correctly2 = () => consoleMock.Verify(x => x.WriteLine(Moq.It.IsAny<string>(), "ist"), Times.Once());
        private It maps_all_words_correctly3 = () => consoleMock.Verify(x => x.WriteLine(Moq.It.IsAny<string>(), "ein"), Times.Once());
    }
}
