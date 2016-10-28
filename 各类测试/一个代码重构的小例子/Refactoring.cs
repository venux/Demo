using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 各类测试.一个代码重构的小例子
{
    public class RefactoringTest
    {
    }


    #region 重构前的代码

    /*
       问题列表：
       •命名问题 - 我们只能通过猜测这个类到底是为了计算什么。这实在是在浪费时间。
               如果我们没有相关文档或者重构这段代码，那我们下一次恐怕需要花大量的时间才能知道这段代码的具体含义。

       •魔数 - 在这个例子中，你能猜到变量type是指客户账户的状态吗。通过if-else来选择计算优惠后的产品价格。
               现在，我们压根不知道账户状态1，2，3，4分别是什么意思。
               此外，你知道0.1，0.7，0.5都是什么意思吗？
               让我们想象一下，如果你要修改下面这行代码：
               result = (amount - (0.5m * amount)) - disc * (amount - (0.5m * amount));

       •隐藏的BUG - 因为代码非常糟糕，我们可能会错过非常重要的事情。试想一下，如果我们的系统中新增了一类账户状态，而新的账户等级不满足任何一个if-else条件。这时，返回值会固定为0。

       •不可读 - 我们不得不承认，这是一段不可读的代码。不可读=更多的理解时间+增加产生错误的风险

       •DRY – 不要产生重复的代码
               我们可能不能一眼就找出它们，但它们确实存在。
               例如：disc *(amount - (0.1m * amount));
               同样的逻辑：
               disc *(amount - (0.5m * amount));
               这里只有一个数值不一样。如果我们无法摆脱重复的代码，我们会遇到很多问题。比如某段代码在五个地方有重复，当我们需要修改这部分逻辑时，你很可能只修改到了2至3处。

       •单一职责原则
               这个类至少具有三个职责：
               1 选择计算算法
               2 根据账户状态计算折扣
               3 根据账户网龄计算折扣
               它违反了单一职责原则。这会带来什么风险?如果我们将要修改第三个功能的话，会影响到另外第二个功能。这就意味着，我们每次修改都会改动我们本不想修改的代码。因此，我们不得不对整个类进行测试，这实在很浪费时间。
    */
    class Class1
    {
        public decimal Calculate(decimal amount, int type, int years)
        {
            decimal result = 0;
            decimal disc = (years > 5) ? (decimal)5 / 100 : (decimal)years / 100;
            if (type == 1)
            {
                result = amount;
            }
            else if (type == 2)
            {
                result = (amount - (0.1m * amount)) - disc * (amount - (0.1m * amount));
            }
            else if (type == 3)
            {
                result = (0.7m * amount) - disc * (0.7m * amount);
            }
            else if (type == 4)
            {
                result = (amount - (0.5m * amount)) - disc * (amount - (0.5m * amount));
            }
            return result;
        }
    }

    #endregion

    #region 重构过程

    #region 第一步：命名

    /*
        这是良好代码的最重要方面之一。我们只需要改变方法，参数和变量的命名。
        现在，我们可以确切的知道下面的类是负责什么的了。
    */
    class Step1
    {
        public decimal ApplyDiscount(decimal price, int accountStatus, int timeOfHavingAccountInYears)
        {
            decimal priceAfterDiscount = 0;
            decimal discountForLayaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5 / 100 : (decimal)timeOfHavingAccountInYears / 100;
            if (accountStatus == 1)
            {
                priceAfterDiscount = price;
            }
            else if (accountStatus == 2)
            {
                priceAfterDiscount = (price - (0.1m * price)) - discountForLayaltyInPercentage * (price - (0.1m * price));
            }
            else if (accountStatus == 3)
            {
                priceAfterDiscount = (0.7m * price) - discountForLayaltyInPercentage * (0.7m * price);
            }
            else if (accountStatus == 4)
            {
                priceAfterDiscount = (price - (0.5m * price)) - discountForLayaltyInPercentage * (price - (0.5m * price));
            }
            return priceAfterDiscount;
        }
    }

    #endregion

    #region  第二步：魔数

    /*
        在C#中避免魔数我们一般采用枚举来替换它们。这里使用AccountStatus 枚举来替换if-else中的魔数。
        现在我们来看看重构后的类，我们可以很容易的说出哪一个账户状态应该用什么算法来计算折扣。混合账户状态的风险迅速的下降了。    
     */
    class Step2
    {
        public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
        {
            decimal priceAfterDiscount = 0;
            decimal discountForLayaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5 / 100 : (decimal)timeOfHavingAccountInYears / 100;
            if (accountStatus == AccountStatus.NotRegistered)
            {
                priceAfterDiscount = price;
            }
            else if (accountStatus == AccountStatus.SimpleCustomer)
            {
                priceAfterDiscount = (price - (0.1m * price)) - discountForLayaltyInPercentage * (price - (0.1m * price));
            }
            else if (accountStatus == AccountStatus.ValuableCustomer)
            {
                priceAfterDiscount = (0.7m * price) - discountForLayaltyInPercentage * (0.7m * price);
            }
            else if (accountStatus == AccountStatus.MostValuableCustomer)
            {
                priceAfterDiscount = (price - (0.5m * price)) - discountForLayaltyInPercentage * (price - (0.5m * price));
            }
            return priceAfterDiscount;
        }
    }

    #endregion

    #region 第三步：更多的代码可读性

    /*
        在这一步中，我们使用switch-case 来替换 if-else if来增强代码可读性。
        同时，我还将某些长度很长的语句才分成两行。   
     */
    class Step3
    {
        public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
        {
            decimal priceAfterDiscount = 0;
            decimal discountForLayaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5 / 100 : (decimal)timeOfHavingAccountInYears / 100;

            switch (accountStatus)
            {
                case AccountStatus.NotRegistered:
                    priceAfterDiscount = price;
                    break;
                case AccountStatus.SimpleCustomer:
                    priceAfterDiscount = (price - (0.1m * price));
                    priceAfterDiscount -= discountForLayaltyInPercentage * (price - (0.1m * price));
                    break;
                case AccountStatus.ValuableCustomer:
                    priceAfterDiscount = (0.7m * price);
                    priceAfterDiscount -= discountForLayaltyInPercentage * (0.7m * price);
                    break;
                case AccountStatus.MostValuableCustomer:
                    priceAfterDiscount = (price - (0.5m * price));
                    priceAfterDiscount -= discountForLayaltyInPercentage * (price - (0.5m * price));
                    break;
            }

            return priceAfterDiscount;
        }
    }

    #endregion

    #region 第四步：消除隐藏的BUG

    /*
        正如我们之前提到的，我们的ApplyDiscount方法可能将为新的客户状态返回0。
        我们怎样才能解决这个问题？答案就是抛出NotImplementedException。
        当我们的方法获取账户状态作为输入参数，但是参数值可能包含我们未设计到的未知情况。这时，我们不能什么也不做，抛出异常是这时候最好的做法。 
     */
    class Step4
    {
        public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
        {
            decimal priceAfterDiscount = 0;
            decimal discountForLayaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5 / 100 : (decimal)timeOfHavingAccountInYears / 100;

            switch (accountStatus)
            {
                case AccountStatus.NotRegistered:
                    priceAfterDiscount = price;
                    break;
                case AccountStatus.SimpleCustomer:
                    priceAfterDiscount = (price - (0.1m * price));
                    priceAfterDiscount -= discountForLayaltyInPercentage * (price - (0.1m * price));
                    break;
                case AccountStatus.ValuableCustomer:
                    priceAfterDiscount = (0.7m * price);
                    priceAfterDiscount -= discountForLayaltyInPercentage * (0.7m * price);
                    break;
                case AccountStatus.MostValuableCustomer:
                    priceAfterDiscount = (price - (0.5m * price));
                    priceAfterDiscount -= discountForLayaltyInPercentage * (price - (0.5m * price));
                    break;
                default:
                    throw new NotImplementedException();
            }

            return priceAfterDiscount;
        }
    }

    #endregion

    #region 第五步：分析算法

    /*
      * 在这个例子中，我们通过两个标准来计算客户折扣：账户状态、账户网龄
      * 通过网龄计算的算法都类似这样：(discountForLoyaltyInPercentage * priceAfterDiscount)
      * 但是对于账户状态为ValuableCustomer的算法却是：0.7m * price。我们把它修改成和其他账户状态一样的算法：price - (0.3m * price)
      */
    class Step5
    {
        public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
        {
            decimal priceAfterDiscount = 0;
            decimal discountForLayaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5 / 100 : (decimal)timeOfHavingAccountInYears / 100;

            switch (accountStatus)
            {
                case AccountStatus.NotRegistered:
                    priceAfterDiscount = price;
                    break;
                case AccountStatus.SimpleCustomer:
                    priceAfterDiscount = (price - (0.1m * price));
                    priceAfterDiscount -= discountForLayaltyInPercentage * priceAfterDiscount;
                    break;
                case AccountStatus.ValuableCustomer:
                    priceAfterDiscount = (price - (0.3m * price));
                    priceAfterDiscount -= discountForLayaltyInPercentage * priceAfterDiscount;
                    break;
                case AccountStatus.MostValuableCustomer:
                    priceAfterDiscount = (price - (0.5m * price));
                    priceAfterDiscount -= discountForLayaltyInPercentage * priceAfterDiscount;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return priceAfterDiscount;
        }
    }

    #endregion

    #region 第六步：消除魔数、消除重复代码、删除没用代码

    /*
     * 干净的代码
     */
    class Step6
    {
        public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
        {
            decimal priceAfterDiscount = 0;
            decimal discountForLayaltyInPercentage = price.ApplyDiscountForTimeOfHavingAccount(timeOfHavingAccountInYears);

            switch (accountStatus)
            {
                case AccountStatus.NotRegistered:
                    priceAfterDiscount = price;
                    break;
                case AccountStatus.SimpleCustomer:
                    priceAfterDiscount = price - (Constants.DISCOUNT_FOR_SIMPLE_CUSTOMERS * price);
                    break;
                case AccountStatus.ValuableCustomer:
                    priceAfterDiscount = price - (Constants.DISCOUNT_FOR_VALUABLE_CUSTOMERS * price);
                    break;
                case AccountStatus.MostValuableCustomer:
                    priceAfterDiscount = price - (Constants.DISCOUNT_FOR_MOST_VALUABLE_CUSTOMERS * price);
                    break;
                default:
                    throw new NotImplementedException();
            }
            priceAfterDiscount -= discountForLayaltyInPercentage * priceAfterDiscount;

            return priceAfterDiscount;
        }
    }

    #endregion

    #endregion

    #region 重构过程中生成的工具

    public enum AccountStatus
    {
        NotRegistered = 1,
        SimpleCustomer = 2,
        ValuableCustomer = 3,
        MostValuableCustomer = 4
    }

    public static class Constants
    {
        public const int MAXIMUM_DISCOUNT_FOR_LOYALTY = 5;
        public const decimal DISCOUNT_FOR_SIMPLE_CUSTOMERS = 0.1m;
        public const decimal DISCOUNT_FOR_VALUABLE_CUSTOMERS = 0.3m;
        public const decimal DISCOUNT_FOR_MOST_VALUABLE_CUSTOMERS = 0.5m;
    }

    public static class PriceExtensions
    {
        public static decimal ApplyDiscountForAccountStatus(this decimal price, decimal discountSize)
        {
            return price - (discountSize * price);
        }

        public static decimal ApplyDiscountForTimeOfHavingAccount(this decimal price, int timeOfHavingAccountInYears)
        {
            decimal discountForLoyaltyInPercentage = (timeOfHavingAccountInYears > Constants.MAXIMUM_DISCOUNT_FOR_LOYALTY) ? (decimal)Constants.MAXIMUM_DISCOUNT_FOR_LOYALTY / 100 : (decimal)timeOfHavingAccountInYears / 100;
            return price - (discountForLoyaltyInPercentage * price);
        }
    }

    #endregion

}
