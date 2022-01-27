using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AxosWebAppi.Data;
using AxosWebAppi.Models;
using AutoMapper;
using AxosWebAppi.Dto;

namespace AxosWebAppi.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CarsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            //return await _context.Cars.ToListAsync();

            var cars = await _context.Cars.ToListAsync();

            return _mapper.Map<List<Car>, List<CarDto>>(cars);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return _mapper.Map<CarDto>(car);
            //return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarDto car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        public async Task<ActionResult<CarDto>> PostCar(CarDto carDto)
        {
            var car = new Car
            {
                Name = carDto.Name,
                Model = carDto.Model,
                Year = carDto.Year
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCar),
                new { id = car.Id },
                CarToDTO(car));
            //return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }



        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }


        private static CarDto CarToDTO(Car car) =>
            new CarDto
            {
                Id = car.Id,
                Name = car.Name,
                Model = car.Model,
                Year = car.Year
            };
    }
}

