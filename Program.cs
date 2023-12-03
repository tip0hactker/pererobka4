public abstract class LivingOrganism
{
    protected int energy;
    protected int age;
    protected int size;

    public LivingOrganism(int energy, int age, int size)
    {
        this.energy = energy;
        this.age = age;
        this.size = size;
    }

    public abstract void eat(LivingOrganism food);
    public abstract void grow();
    public abstract void reproduce();
}

public class Animal : LivingOrganism
{
    protected string diet;

    public Animal(int energy, int age, int size, string diet) : base(energy, age, size)
    {
        this.diet = diet;
    }

    public override void eat(LivingOrganism food)
    {
        if (food.diet == this.diet)
        {
            this.energy += food.energy;
            food.die();
        }
    }

    public override void reproduce()
    {
        int offspring = this.energy / 2;
        List<LivingOrganism> offsprings = new List<LivingOrganism>();
        for (int i = 0; i < offspring; i++)
        {
            offsprings.Add(new Animal(this.energy / (i + 1), this.age, this.size / (i + 1)));
        }
        return offsprings;
    }
}

public class Plant : LivingOrganism
{
    protected string species;

    public Plant(int energy, int age, int size, string species) : base(energy, age, size)
    {
        this.species = species;
    }

    public override void eat()
    {
        this.energy++;
    }

    public override void reproduce()
    {
        int offspring = this.energy / 2;
        List<LivingOrganism> offsprings = new List<LivingOrganism>();
        for (int i = 0; i < offspring; i++)
        {
            offsprings.Add(new Plant(this.energy / (i + 1), this.age, this.size / (i + 1), this.species));
        }
        return offsprings;
    }
}


public class Microorganism : LivingOrganism
{
    protected int reproductionRate;

    public Microorganism(int energy, int age, int size, int reproductionRate) : base(energy, age, size)
    {
        this.reproductionRate = reproductionRate;
    }

    public override void reproduce()
    {
        List<LivingOrganism> offsprings = new List<LivingOrganism>();
        for (int i = 0; i < this.reproductionRate; i++)
        {
            offsprings.Add(new Microorganism(this.energy / (i + 1), this.age, this.size / (i + 1), this.reproductionRate));
        }
        return offsprings;
    }
}

public class Ecosystem
{
    protected List<LivingOrganism> organisms;

    public Ecosystem(List<LivingOrganism> organisms)
    {
        this.organisms = organisms;
    }

    public void simulate(int steps)
    {
        for (int step = 0; step < steps; step++)
        {
            foreach (LivingOrganism organism in this.organisms)
            {
                organism.eat();
                organism.grow();
                if (organism is Animal)
                {
                    ((Animal)organism).hunt();
                }
                else if (organism is Plant)
                {
                    ((Plant)organism).photosynthesis();
                }
                else if (organism is Microorganism)
                {
                    ((Microorganism)organism).reproduce();
                }
            }
        }
    