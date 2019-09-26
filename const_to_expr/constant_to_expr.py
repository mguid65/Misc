import sys
import os
import random
import argparse
from functools import reduce

l_or_r = True

def factors(n):
    return list(reduce(list.__add__,
                ([i, n//i] for i in range(1, int(n**0.5) + 1) if n % i == 0)))

possible_obfuscations = []

def num_to_mult_expr(num, depth, limit):
  global l_or_r, possible_obfuscations
  l_or_r = not l_or_r

  expected_res = num
  fact = factors(expected_res)
  x = random.choice(fact)
  y = expected_res / x;

  if depth == limit: return '(' + str(x) + ' * ' + str(y) + ')'
  if l_or_r: return '(' + str(x) + ' * ' + random.choice(possible_obfuscations)(y, depth+1, limit) + ')'
  else: return '(' + random.choice(possible_obfuscations)(x, depth+1, limit) + ' * ' + str(y) + ')'

def num_to_div_expr(num, depth, limit):
  global l_or_r, possible_obfuscations
  l_or_r = not l_or_r

  expected_res = num
  fact = factors(expected_res)
  y = random.choice(fact)
  x = y * expected_res

  if depth == limit: return '(' + str(x) + ' / ' + str(y) + ')'
  if l_or_r: return '(' + str(x) + ' / ' + random.choice(possible_obfuscations)(y, depth+1, limit) + ')'
  else: return '(' + random.choice(possible_obfuscations)(x, depth+1, limit) + ' / ' + str(y) + ')'


def num_to_add_expr(num, depth, limit):
  global l_or_r, possible_obfuscations
  l_or_r = not l_or_r

  expected_res = num
  x = random.randint(1, num)
  y = expected_res - x

  if depth == limit: return '(' + str(x) + ' + ' + str(y) + ')'
  if l_or_r: return '(' + str(x) + ' + ' + random.choice(possible_obfuscations)(y, depth+1, limit) + ')'
  else: return '(' + random.choice(possible_obfuscations)(x, depth+1, limit) + ' + ' + str(y) + ')'

def num_to_sub_expr(num, depth, limit):
  global l_or_r, possible_obfuscations
  l_or_r = not l_or_r

  expected_res = num
  x = random.randint(1, num)
  y = expected_res + x

  if depth == limit: return '(' + str(x) + ' - ' + str(y) + ')'
  if l_or_r: return '(' + str(x) + ' - ' + random.choice(possible_obfuscations)(y, depth+1, limit) + ')'
  else: return '(' + random.choice(possible_obfuscations)(x, depth+1, limit) + ' - ' + str(y) + ')'

possible_obfuscations = [ num_to_mult_expr, num_to_div_expr, num_to_add_expr, num_to_sub_expr ]

def obfuscate(input, limit):
  return random.choice(possible_obfuscations)(input, 0, limit - 2)

def main():
  parser = argparse.ArgumentParser(description='Expand a constant into an expression')
  parser.add_argument('-c', '--constant', help="Constant Value", required=True)
  parser.add_argument('-l', '--limit', help="Expression depth limit", type=int, default=5)
  parser.add_argument('-f', '--float', help="Is this a float constant", default=False, action='store_true')

  args = parser.parse_args()

  if args.float:
    print(obfuscate(float(args.constant), int(args.limit)))
  else:
    print(obfuscate(int(args.constant), int(args.limit)))

if __name__ == '__main__':
  main()
